using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private List<OceanFishController> _sensedFishes = new List<OceanFishController>();

    private OceanFishController _catchedFish = null;
    public OceanFishController CatchedFish
    {
        get => _catchedFish;
        set => _catchedFish = value;
    }

    private void Update()
    {
        if (!StartCheck)
            return;
        
        SensingUnit();
    }

    private void SensingUnit()
    {
        RaycastHit[] _hits = Physics.SphereCastAll(transform.position, _radius, Vector3.up, 0, _layerMask);

        foreach (var sensedFish in _sensedFishes.ToList())
        {
            if (_catchedFish == sensedFish) _catchedFish = null;
            if (!_hits.Any(hit => hit.collider.gameObject == sensedFish.gameObject))
            {
                sensedFish.ActionData.IsSensed = false;
                sensedFish.ActionData.IsCatch = false;
                _sensedFishes.Remove(sensedFish);
            }
        }

        if (_hits.Length <= 0 || _catchedFish != null) return;

        foreach (RaycastHit hit in _hits)
        {
            if (hit.collider.TryGetComponent<OceanFishController>(out var fish))
            {
                fish.ActionData.IsSensed = true;
                fish.ActionData.BaitTrm = transform;
                fish.GetModule<FishMovementModule>().Turn();

                if (!_sensedFishes.Contains(fish))
                {
                    _sensedFishes.Add(fish);
                }

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
