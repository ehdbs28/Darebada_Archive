using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Facility :MonoBehaviour
{
    public abstract Facility OnTouched();
    public bool isCollision;
    [SerializeField] protected LayerMask _layerMask;
    [SerializeField] protected Collider _collider;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
    public bool CheckCollision()
    {
        Collider[] hit = Physics.OverlapBox(_collider.bounds.center, _collider.bounds.size/2, Quaternion.identity, _layerMask);
        if (hit.Length >=2)
        {
            return false;
        }
        return true;
    }
}
