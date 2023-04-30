using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Core.CameraState;

public class FishingIdleState : FishingState
{
    private Transform _bobberTrm;
    private Transform _playerTrm;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);

        _controller.ActionData.IsFishing = false;
        _bobberTrm = agentRoot.Find("Bobber");
        _playerTrm = agentRoot.parent;
        _controller.ActionData.InitPosition = _bobberTrm.position;
    }

    public override void EnterState()
    {
        _bobberTrm.position = _controller.ActionData.InitPosition;
        CameraManager.Instance.SetVCam(BOAT_FOLLOW);
    }

    public override void ExitState()
    {
        _controller.ActionData.IsFishing = true;
    }
}
