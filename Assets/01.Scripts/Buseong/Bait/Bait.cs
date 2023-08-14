using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    [SerializeField]
    private float _radius;
    
    [SerializeField]
    private LayerMask _layerMask;

    public bool Sense
    {
        get
        {
            if (_catchedFish == null)
                return false;
            
            return _catchedFish.ActionData.IsCatch;
        }
    }
    
    public bool StartCheck { get; set; }

    private OceanFishController _catchedFish = null;
    public OceanFishController CatchedFish => _catchedFish;

    private void Update()
    {
        if (!StartCheck)
            return;
        
        SensingUnit();
    }

    private void SensingUnit()
    {
        RaycastHit[] _hits = Physics.SphereCastAll(transform.position, _radius, Vector3.up, 0, _layerMask);
        if (_hits.Length <= 0 || _catchedFish != null) return;

        foreach (RaycastHit hit in _hits)
        {
            if (hit.collider.TryGetComponent<OceanFishController>(out var fish))
            {
                fish.ActionData.IsSensed = true;
                fish.ActionData.BaitTrm = transform;
                fish.GetModule<FishMovementModule>().Turn();
                _catchedFish = fish;
                
                break;
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
#endif
}
