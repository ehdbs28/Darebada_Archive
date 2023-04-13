using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableState : CommonState
{
    public override void OnEnterState()
    {
        _agentAnimator.SetDieState(true);
    }

    public override void OnExitState()
    {
    }

    public override void UpdateState()
    {
    }
}
