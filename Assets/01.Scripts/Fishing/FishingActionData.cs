using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingActionData : MonoBehaviour
{
    // 마지막 차징 힘 저장 용도
    [HideInInspector]
    public float LastChargingPower;

    [HideInInspector]
    public bool IsFishing = false;

    [HideInInspector]
    public bool IsUnderWater = false;

    [HideInInspector]
    public Vector3 LastThrowDirection;

    [HideInInspector]
    public Vector3 InitPosition;
}
