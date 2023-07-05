using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public float MaxSpeed = 5f;
    public float Acceleration = 10f;
    public float Deceleration = 10f;
}
