using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libra : MonoBehaviour
{
    private WeightObj _weightObj;
    private float _libraWeight;
    private float _height;
    private Collider[] hit;
    private Transform _rayPos;
    [SerializeField]
    GameObject upLibra, downLibra;
    [SerializeField]
    private float _targetHeight;

    private void Awake()
    {
        _weightObj = GetComponent<WeightObj>();
    }

    private void Update()
    {
        hit = Physics.OverlapBox(_rayPos.position,
            new Vector3((upLibra.transform.lossyScale).x, 0.1f, (upLibra.transform.lossyScale).z), 
            upLibra.transform.rotation, _weightObj.layerMask); 
        if(hit.Length > 0)
        {

        }
    }
}
