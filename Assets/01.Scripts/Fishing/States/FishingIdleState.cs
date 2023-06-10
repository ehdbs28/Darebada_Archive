using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingIdleState : FishingState
{
    private Transform _bobberTrm;
    private Transform _playerTrm;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);

        _bobberTrm = agentRoot.Find("Bobber");
        _playerTrm = agentRoot.parent;
        _controller.ActionData.InitPosition = _bobberTrm.position;
    }

    public override void EnterState()
    {
        GameManager.Instance.GetManager<UIManager>().ShowPanel(ScreenType.Ocean);

        _controller.ActionData.IsFishing = false;
        _bobberTrm.position = _controller.ActionData.InitPosition;
        GameManager.Instance.GetManager<CameraManager>().SetVCam(CameraState.BOAT_FOLLOW);
    }

    public override void ExitState()
    {
        _controller.ActionData.IsFishing = true;
    }
}
