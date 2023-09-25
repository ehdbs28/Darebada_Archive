using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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

    public bool IsCollision()
    {
        Collider[] cols = Physics.OverlapBox(_collider.bounds.center, _collider.bounds.size/2, Quaternion.identity, _layerMask);
        for(int i = 0; i < cols.Length; i++)
        {
                Debug.Log(cols[i].gameObject.name);
                //Destroy(cols[i].gameObject);
            
        }
        Debug.Log(cols.Length);
        return cols.Length < 2;
    }
}
