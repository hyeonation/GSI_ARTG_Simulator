using UnityEngine;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Jobs;

public class SPSSManager : MonoBehaviour
{
    [Header("Attached Sensors")]
    public SPSSLIDAR[] sensors;

    // 통합 데이터 버퍼
    private NativeArray<float3> _allPoints;
    private int _totalCapacity;

    void Start()
    {

    }


}