using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingIdleState : FishingState
{
    [SerializeField]
    private Transform _bobberPos;

    private Transform _bobberTrm;
    private Transform _playerTrm;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);

        _bobberTrm = agentRoot.Find("Bobber");
        _playerTrm = agentRoot;
    }

    public override void EnterState()
    {
        GameManager.Instance.GetManager<UIManager>().ShowPanel(ScreenType.Ocean);

        if (_controller.Bait.CatchedFish != null)
        {
            ((CatchedFishCheckingPopup)GameManager.Instance.GetManager<UIManager>().GetPanel(PopupType.CatchedFishChecking)).dataUnit = _controller.Bait.CatchedFish.DataUnit;
            GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.CatchedFishChecking);
            _controller.Bait.CatchedFish = null;
        }

        _controller.ActionData.IsFishing = false;
        GameManager.Instance.GetManager<CameraManager>().SetVCam(CameraState.BOAT_FOLLOW);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _bobberTrm.position = _bobberPos.position;
        _bobberTrm.rotation = Quaternion.LookRotation(Vector3.down);
    }

    public override void ExitState()
    {
        _controller.ActionData.IsFishing = true;
    }
}
