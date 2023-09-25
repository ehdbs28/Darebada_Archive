using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightObj : MonoBehaviour
{
    [SerializeField]
    float weight;
    public LayerMask layerMask;

    private void Awake()
    {
        layerMask = LayerMask.GetMask("Weight_Obj");
    }
}
