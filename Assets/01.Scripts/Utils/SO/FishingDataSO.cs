using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/FishingData")]
public class FishingDataSO : ScriptableObject
{
    public float ChargingSpeed = 5f;

    public float ThrowingSpeed = 3f;

    public float StringStrength = 1f;
}
