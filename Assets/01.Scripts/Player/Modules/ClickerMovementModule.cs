using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickerMovementModule : CommonModule<PlayerController>
{
    private NavMeshAgent _navAgent;

    public override void SetUp(Transform rootTrm)
    {
        base.SetUp(rootTrm);

        _navAgent = rootTrm.GetComponent<NavMeshAgent>();
    }

    public override void FixedUpdateModule()
    {
    }

    public override void UpdateModule()
    {
    }
}