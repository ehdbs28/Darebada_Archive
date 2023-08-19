using UnityEngine;
using UnityEngine.Serialization;

public class FishActionData : MonoBehaviour
{
    public Transform BaitTrm = null;
    
    public bool IsSensed = false;
    public bool IsCatch = false;

    public float Lenght = 0;
    public float Weight = 0;
}
