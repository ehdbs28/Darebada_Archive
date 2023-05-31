using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingThrowingState : FishingState
{
    private Transform _bobberTrm;
    private Transform _playerTrm;

    private float _gravity = -9.8f;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);

        _bobberTrm = agentRoot.Find("Bobber");
        _playerTrm = agentRoot.parent;
    }

    public override void EnterState()
    {
        _playerTrm.LookAt(_controller.ActionData.LastThrowDirection);
        _controller.ActionData.InitPosition = _bobberTrm.position;

        GameManager.Instance.GetManager<CameraManager>().SetVCam(CameraState.BOBBER_FOLLOW);

        StartCoroutine(ThrowTo());
    }

    public override void ExitState()
    {
    }

    private IEnumerator ThrowTo(){
        Vector3 start = _bobberTrm.position;
        Vector3 end = _bobberTrm.position + (_controller.ActionData.LastThrowDirection.normalized * _controller.ActionData.LastChargingPower);
        end.y = 0;

        float throwTime = Mathf.Max(0.3f, Vector3.Distance(start, end)) / _controller.FishingData.ThrowingSpeed;

        float currentTime = 0f;
        float percent = 0f;

        float v0 = (end - start).y - _gravity;

        while(percent < 1){
            currentTime += _controller.FishingData.ThrowingSpeed * Time.deltaTime;
            percent = currentTime / throwTime;

            Vector3 pos = Vector3.Lerp(start, end, percent);
            pos.y = start.y + (v0 * percent) +  (_gravity * percent * percent);
            _bobberTrm.position = pos;

            yield return null;
        }

        _controller.ActionData.IsUnderWater = true;
    }
}
