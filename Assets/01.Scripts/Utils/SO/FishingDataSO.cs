using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/FishingData")]
public class FishingDataSO : ScriptableObject
{
    public float ChargingSpeed = 5f;
    public float MaxChargingPower = 10f;

    public float RotationSpeed = 360f;

    public float ThrowingSpeed = 3f;

    public float StringLength = 10f;

    public float StringStrength = 1f;
}
