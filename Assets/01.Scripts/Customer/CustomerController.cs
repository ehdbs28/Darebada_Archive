using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : ModuleController
{
    private NavMeshAgent _navAgent;
    public NavMeshAgent NavAgent => _navAgent;

    protected override void Awake()
    {
        base.Awake();
        _navAgent = GetComponent<NavMeshAgent>();
    }
}
