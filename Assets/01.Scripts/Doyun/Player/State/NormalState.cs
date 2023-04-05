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
    }
}
