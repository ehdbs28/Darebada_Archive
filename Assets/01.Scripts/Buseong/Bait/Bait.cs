using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    [SerializeField]
    private float _radius;
    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private RaycastHit[] _hits;
    private RaycastHit _hit;

    [SerializeField]
    private bool sense;
    public bool Sense
    {
        get => sense;
        set => sense = value;
    }

    private void Update()
    {
        SensingUnit();
    }

    private void SensingUnit()
    {
        //bool sensing = Physics.SphereCast(transform.position, _radius, Vector3.up, out _hit, 0f, _layerMask);
        //if (!sensing) return;

        //_hit.collider.GetComponent<BoidUnit>().IsSensed = true;
        //if(Vector3.Distance(_hit.collider.transform.position, transform.position) < 1.5f)
        //{
        //    _hit.collider.GetComponent<BoidUnit>().IsBite = true;
        //}

        

        _hits = Physics.SphereCastAll(transform.position, _radius, Vector3.up, 0, _layerMask);
        if (_hits.Length <= 0 || sense) return;

        foreach (RaycastHit hit in _hits)
        {
            hit.collider.GetComponent<BoidUnit>().IsSensed = true;
            if (Vector3.Distance(hit.collider.transform.position, transform.position) < 3f)
            {
                sense = true;
                hit.collider.GetComponent<BoidUnit>().IsBite = true;
                foreach(RaycastHit nhit in _hits)
                {
                    nhit.collider.GetComponent<BoidUnit>().IsSensed = false;
                }
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
