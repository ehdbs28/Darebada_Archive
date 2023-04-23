using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatData : MonoBehaviour
{
    [Header("보트 이동 관련")]
    [Tooltip("보트의 최대 이동속도")]
    [Range(1f, 10f)]
    public float BoatMaxSpeed;

    [Tooltip("보트 전진 가속도")]
    [Range(0.1f, 100f)]
    public float BoatForwardAcceleration;

    [Tooltip("보트 후진 가속도")]
    [Range(0.1f, 100f)]
    public float BoatBackwardAcceleration;

    [Tooltip("보트 감속도")]
    [Range(0.1f, 100f)]
    public float BoatDeceleration;

    [Header("보트 회전 관련")]
    [Tooltip("보트의 최대 로테이션속도")]
    [Range(1f, 10f)]
    public float BoatMaxRotationSpeed;
    
    [Tooltip("보트 회전 가속도")]
    [Range(0.1f, 100f)]
    public float BoatRotationAcceleration;

    [Tooltip("보트 회전 감속도")]
    [Range(0.1f, 100f)]
    public float BoatRotationDeceleration;

    [Header("보트 연료 관련")]
    [Tooltip("보트의 최대 연료량이다")]
    public float MaxFule;

    [Header("보트 내구도 관련")]
    [Tooltip("보트의 최대 내구도이다")]
    public float MaxDurablity;
}
