using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using Unity.Burst;
using Unity.Mathematics;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SPSSLIDAR : MonoBehaviour
{
    [Header("Hardware Settings")]
    public float lidarFovHorizontal_deg = 90f;
    public float lidarFovVertical_deg = 42.4f;
    public float lidarResHorizontal_deg = 0.5f;
    public float lidarResVertical_deg = 0.5f;
    public float lidarMaxDistance_m = 100f;

    [Header("Axis Correction")]
    [Range(-180, 180)] public float axisRotationOffset_deg = 0f;

    [Header("Local ROI Settings")]
    public bool useROI = true;
    public Vector3 localRoiMin = new Vector3(-10, -2, 0);
    public Vector3 localRoiMax = new Vector3(10, 5, 50);

    private NativeArray<RaycastCommand> _commands;
    private NativeArray<RaycastHit> _hits;
    private NativeArray<float3> _points;
    private JobHandle _jobHandle;
    private int _hSteps, _vSteps, _totalSteps;
    private Mesh _mesh;

    public NativeArray<float3> GetPoints() => _points;
    public int TotalPoints => _totalSteps;
    public JobHandle CurrentJobHandle => _jobHandle;

    void Awake()
    {
        _mesh = new Mesh { name = "LiDAR_Cloud", indexFormat = UnityEngine.Rendering.IndexFormat.UInt32 };
        GetComponent<MeshFilter>().sharedMesh = _mesh;
    }

    void Start() => Reinitialize();

    public void Reinitialize()
    {
        _hSteps = Mathf.Max(1, Mathf.CeilToInt(lidarFovHorizontal_deg / lidarResHorizontal_deg));
        _vSteps = Mathf.Max(1, Mathf.CeilToInt(lidarFovVertical_deg / lidarResVertical_deg));
        _totalSteps = _hSteps * _vSteps;

        if (_commands.IsCreated) DisposeMemory();
        _commands = new NativeArray<RaycastCommand>(_totalSteps, Allocator.Persistent);
        _hits = new NativeArray<RaycastHit>(_totalSteps, Allocator.Persistent);
        _points = new NativeArray<float3>(_totalSteps, Allocator.Persistent);

        // 1. 인덱스 배열 생성
        int[] indices = new int[_totalSteps];
        for (int i = 0; i < _totalSteps; i++) indices[i] = i;

        // 2. 중요: 인덱스를 설정하기 전에 메쉬의 버텍스 카운트를 미리 확보해야 함
        // 빈 버텍스 배열을 할당하여 Mesh에게 공간이 있음을 알림
        _mesh.Clear(); // 기존 데이터 초기화
        _mesh.vertices = new Vector3[_totalSteps];

        // 3. 인덱스 설정 (이제 VertexCount가 _totalSteps이므로 에러가 나지 않음)
        _mesh.SetIndices(indices, MeshTopology.Points, 0);
        _mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 1000f);
    }

    public void ScheduleScan()
    {
        var setJob = new SetRaycastJob
        {

            // 위치나 회전은 부모오브젝트가 하는중
            origin = transform.parent.position,
            rotation = transform.parent.rotation,
            axisOffset = math.radians(axisRotationOffset_deg),
            maxDistance = lidarMaxDistance_m,
            resV_Rad = math.radians(lidarResVertical_deg),
            resH_Rad = math.radians(lidarResHorizontal_deg),
            hStart_Rad = math.radians(-lidarFovHorizontal_deg * 0.5f),
            vStart_Rad = math.radians(-lidarFovVertical_deg * 0.5f),
            hSteps = _hSteps,
            layerMask = -1,
            commands = _commands
        };

        _jobHandle = setJob.Schedule(_totalSteps, 64);
        _jobHandle = RaycastCommand.ScheduleBatch(_commands, _hits, 128, _jobHandle);

        var collectJob = new CollectLocalFilterJob
        {
            hits = _hits,
            commands = _commands,
            maxDistance = lidarMaxDistance_m,
            lidarOrigin = transform.position,
            lidarRotInverse = math.inverse(transform.rotation),
            useROI = useROI,
            roiMin = localRoiMin,
            roiMax = localRoiMax,
            points = _points
        };

        _jobHandle = collectJob.Schedule(_totalSteps, 64, _jobHandle);
    }

    public void UpdateMesh()
    {
        if (!_points.IsCreated) return;
        _mesh.SetVertices(_points.Reinterpret<Vector3>());
        _mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 1000f);
    }

    public void ClearMesh() { if (_mesh != null) _mesh.Clear(); }

    void OnDestroy() => DisposeMemory();
    private void DisposeMemory()
    {
        if (_commands.IsCreated) _commands.Dispose();
        if (_hits.IsCreated) _hits.Dispose();
        if (_points.IsCreated) _points.Dispose();
    }

    [BurstCompile]
    struct SetRaycastJob : IJobParallelFor
    {
        public float3 origin; public quaternion rotation; public float axisOffset;
        public float maxDistance, resV_Rad, resH_Rad, hStart_Rad, vStart_Rad;
        public int hSteps, layerMask;
        public void Execute(int i)
        {
            float vAng = vStart_Rad + (i / hSteps * resV_Rad);
            float hAng = hStart_Rad + (i % hSteps * resH_Rad);
            float cV = math.cos(vAng);
            float3 localDir = new float3(cV * math.sin(hAng), math.sin(vAng), cV * math.cos(hAng));
            localDir = math.mul(quaternion.Euler(0, 0, axisOffset), localDir);
            float3 dir = math.mul(rotation, localDir);
            commands[i] = new RaycastCommand(origin, dir, new QueryParameters(layerMask), maxDistance);
        }
        [WriteOnly] public NativeArray<RaycastCommand> commands;
    }

    [BurstCompile]
    struct CollectLocalFilterJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<RaycastHit> hits;
        [ReadOnly] public NativeArray<RaycastCommand> commands;
        public float maxDistance;
        public float3 lidarOrigin; public quaternion lidarRotInverse;
        public bool useROI; public float3 roiMin, roiMax;
        public void Execute(int i)
        {
            float dist = hits[i].distance > 0 ? hits[i].distance : maxDistance;
            float3 worldPos = (float3)commands[i].from + ((float3)commands[i].direction * dist);
            float3 localPos = math.mul(lidarRotInverse, worldPos - lidarOrigin);
            if (useROI && (localPos.x < roiMin.x || localPos.x > roiMax.x || localPos.y < roiMin.y || localPos.y > roiMax.y || localPos.z < roiMin.z || localPos.z > roiMax.z))
            {
                points[i] = float3.zero;
            }
            else
            {
                points[i] = localPos;
            }
        }
        public NativeArray<float3> points;
    }
}