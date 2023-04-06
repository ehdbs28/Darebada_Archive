using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : CommonState
{
    public override void OnEnterState(){
        _agentInput.OnMouseClickEvent += _agentMovement.SetDestination;
    }

    public override void OnExitState(){
        _agentInput.OnMouseClickEvent -= _agentMovement.SetDestination;
    }

    public override void UpdateState(){
        // 이 부분은 Nav 오류 해결한 다음에 다시 봐야할듯
        _agentAnimator.SetWalkState(_agentMovement.NavMeshAgent.isStopped);
        _agentAnimator.SetGroundState(_agentMovement.CharacterController.isGrounded);
    }
}
