using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Facility :MonoBehaviour
{
    public bool isCollision;
    
    [SerializeField] protected LayerMask _layerMask;
    [SerializeField] protected Collider _collider;
    [SerializeField] protected LayerMask _wallLayer;

    public FacilityType type;
    
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
}
