using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingIdleState : FishingState
{
    private Transform _bobberTrm;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);

        _controller.ActionData.IsFishing = false;
        _bobberTrm = agentRoot.Find("Bobber");
        _controller.ActionData.InitPosition = _bobberTrm.position;
    }

    public override void EnterState()
    {
        Debug.Log(_controller.ActionData.InitPosition);
        _bobberTrm.position = _controller.ActionData.InitPosition;
        Debug.Log(_bobberTrm.position);
    }

    public override void ExitState()
    {
        _controller.ActionData.IsFishing = true;
        _controller.ActionData.InitPosition = _bobberTrm.position;
    }
}
