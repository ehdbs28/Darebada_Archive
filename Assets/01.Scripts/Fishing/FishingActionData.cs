using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingActionData : MonoBehaviour
{
    // 마지막 차징 힘 저장 용도
    public float LastChargingPower;

    public bool IsFishing = false;
    public bool IsUnderWater = false;

    public Vector3 LastThrowDirection;
    public Vector3 InitPosition;

    // 나중에 fish 구조를 바꾸던지 하자
    public FishMovement CurrentCatchFish = null;
}
