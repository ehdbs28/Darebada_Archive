using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerDefine;

public class NormalState : CommonState
{
    public override void OnEnterState(){
        _canUpdateState = true;

        _agentMovement.StopImmediately();

        _agentCondition.OnMouseClickEvent += SetDestination;
        _agentCondition.OnOffMeshJump += HandleJump;
        _agentCondition.OnOffMeshClimb += HandleClimb;
    }

    public override void OnExitState(){
        _canUpdateState = false;

        _agentCondition.OnMouseClickEvent -= SetDestination;
        _agentCondition.OnOffMeshJump -= HandleJump;
        _agentCondition.OnOffMeshClimb -= HandleClimb;

        _agentAnimator.SetWalkState(false);
        _agentAnimator.SetGroundState(true);
    }

    public override void UpdateState(){
        if(!_canUpdateState) return;

        _agentAnimator.SetWalkState(!_agentMovement.IsArrivedCheck());
        _agentAnimator.SetGroundState(_agentMovement.IsGroundedCheck());
    }

    private void SetDestination(Vector3 target){
        _agentMovement.StopImmediately();
        _agentMovement.SetDestination(target);
    }

    private void HandleClimb(){
        _agentController.ChangeState(StateType.Climb);
    }

    private void HandleJump(){
        _agentController.ChangeState(StateType.Jump);
    }
}
