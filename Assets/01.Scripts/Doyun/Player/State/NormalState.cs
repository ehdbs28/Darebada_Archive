using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : CommonState
{
    public override void OnEnterState(){
        _agentInput.OnMouseClickEvent += SetDestination;
    }

    public override void OnExitState(){
        _agentInput.OnMouseClickEvent -= SetDestination;
    }

    private void SetDestination(Vector3 target){
        _agentMovement.StopImmediately();
        _agentMovement.SetDestination(target);
    }

    public override void UpdateState(){
        _agentAnimator.SetWalkState(_agentMovement.NavMeshAgent.pathPending || _agentMovement.NavMeshAgent.remainingDistance > _agentMovement.NavMeshAgent.stoppingDistance);
        _agentAnimator.SetGroundState(true);
    }
}
