using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FishingActionData : MonoBehaviour
{
    // 마지막 차징 힘 저장 용도
    public float LastChargingPower;

    public bool IsFishing = false;
    public bool IsUnderWater = false;

    public bool IsThrowing = false;

    public Vector3 LastThrowDirection;

    // 나중에 fish 구조를 바꾸던지 하자
    public FishMovement CurrentCatchFish = null;
}
