using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingActionData : MonoBehaviour
{
    // ë§ˆì?ë§?ì°¨ì§• ???€???©ë„
    public float LastChargingPower;

    public bool IsFishing = false;
    public bool IsUnderWater = false;

    public bool IsThrowing = false;

    public Vector3 LastThrowDirection;

    // ?˜ì¤‘??fish êµ¬ì¡°ë¥?ë°”ê¾¸?˜ì? ?˜ì
    public FishMovement CurrentCatchFish = null;
}
