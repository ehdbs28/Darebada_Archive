using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingActionData : MonoBehaviour
{
    [Header("낚싯대 차징 관련 변수들")]

    [Tooltip("차징의 스피드")]
    public float ChargingSpeed;

    [Tooltip("최대 차징시 힘")]
    public float MaxChargingPower;

    // 마지막 차징 힘 저장 용도
    [HideInInspector]
    public float LastChargingPower;

    [Header("낚싯대 회전 관련")]

    [Tooltip("회전 속도")]
    public float RotationSpeed;

    [HideInInspector]
    public Vector3 LastThrowDirection;
}
