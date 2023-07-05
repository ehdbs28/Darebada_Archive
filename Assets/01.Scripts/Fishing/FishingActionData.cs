using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingActionData : MonoBehaviour
{
    // 마�?�?차징 ???�???�도
    public float LastChargingPower;

    public bool IsFishing = false;
    public bool IsUnderWater = false;

    public bool IsThrowing = false;

    public Vector3 LastThrowDirection;

    // ?�중??fish 구조�?바꾸?��? ?�자
    public FishMovement CurrentCatchFish = null;
}
