using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatActionData : MonoBehaviour
{
    public Vector3 Forward;

    public bool IsMoveBoat;
    
    [field:SerializeField]
    public bool IsDestroy { get; set; }

    public float CurrentFuel;
}
