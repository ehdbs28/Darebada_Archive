using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoatInput : MonoBehaviour
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] LayerMask _layerMask;
    public ItemSO item;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
            {
                _agent.SetDestination(hit.point);
            }
        }
    }
}
