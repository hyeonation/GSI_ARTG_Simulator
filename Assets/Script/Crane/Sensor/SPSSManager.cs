using UnityEngine;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Jobs;

public class SPSSManager : MonoBehaviour
{
    [System.Serializable]
    public struct LidarSettings
    {
        public SPSSLIDAR lidar;
        public bool isRunning;
        public bool showVisual;
    }

    public LidarSettings[] lidarList;
    private NativeArray<float3> _allPoints;
    private int _totalCapacity;

    void Start()
    {
        if (lidarList == null || lidarList.Length == 0)
        {
            var found = GetComponentsInChildren<SPSSLIDAR>();
            lidarList = new LidarSettings[found.Length];
            for (int i = 0; i < found.Length; i++)
                lidarList[i] = new LidarSettings { lidar = found[i], isRunning = true, showVisual = true };
        }
        foreach (var s in lidarList) if (s.lidar != null) _totalCapacity += s.lidar.TotalPoints;
        _allPoints = new NativeArray<float3>(_totalCapacity, Allocator.Persistent);
    }

    void Update()
    {
        foreach (var s in lidarList)
            if (s.lidar != null && s.isRunning) s.lidar.ScheduleScan();
    }

    void LateUpdate()
    {
        int offset = 0;
        for (int i = 0; i < lidarList.Length; i++)
        {
            var config = lidarList[i];
            if (config.lidar == null) continue;
            if (config.isRunning)
            {
                config.lidar.CurrentJobHandle.Complete();
                if (config.showVisual) config.lidar.UpdateMesh();
                else config.lidar.ClearMesh();
                NativeArray<float3>.Copy(config.lidar.GetPoints(), 0, _allPoints, offset, config.lidar.TotalPoints);
            }
            offset += config.lidar.TotalPoints;
        }
    }

    void OnDestroy() { if (_allPoints.IsCreated) _allPoints.Dispose(); }
}