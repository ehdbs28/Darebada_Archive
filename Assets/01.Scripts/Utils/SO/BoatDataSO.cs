using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BoatData")]
public class BoatDataSO : ScriptableObject
{
    public float BoatMaxSpeed = 5f; 
    public float BoatForwardAcceleration = 10f;
    public float BoatBackwardAcceleration = 3f;
    public float BoatDeceleration = 5f;

    public float BoatMaxRotationSpeed = 1f;
    public float BoatRotationAcceleration = 10f;
    public float BoatRotationDeceleration = 10f;

    public float MaxFuel = 100f;
}
