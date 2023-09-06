using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingThrowingState : FishingState
{
    [SerializeField]
    private Transform _bobberPos;

    private Transform _bobberTrm;
    private Transform _playerTrm;

    private Vector3 _startPos;

    private float _gravity = -9.8f;

    private bool _isThrowing = false;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);

        _bobberTrm = agentRoot.Find("Bobber");
        _playerTrm = agentRoot;
    }

    public override void EnterState()
    {
        //던졌을 때
        GameManager.Instance.GetManager<CameraManager>()._isCanRotate = false;
        _isThrowing = false;
        _startPos = _bobberTrm.position;
        _playerTrm.LookAt(_controller.ActionData.LastThrowDirection);
        _controller.AnimatorController.OnAnimationEvent += OnAnimationEndHandle;
        _controller.AnimatorController.SetCasting(true);
    }

    public override void UpdateModule()
    {
        base.UpdateModule();

        if(_isThrowing == false){
            _bobberTrm.position = _bobberPos.position;
            _bobberTrm.rotation = Quaternion.LookRotation(Vector3.down);
        }
    }

    public override void ExitState()
    {
        // 던지는거 나갈때
        _controller.AnimatorController.OnAnimationEvent -= OnAnimationEndHandle;
    }

    private IEnumerator ThrowTo(){
        Vector3 start = _bobberTrm.position;
        Vector3 end = _startPos + (_controller.ActionData.LastThrowDirection.normalized * _controller.ActionData.LastChargingPower);
        end.y = 0;

        float throwTime = Mathf.Max(0.3f, Vector3.Distance(start, end)) / _controller.DataSO.ThrowingSpeed;

        float currentTime = 0f;
        float percent = 0f;

        float v0 = (end - start).y - _gravity;

        while(percent < 1){
            currentTime += _controller.DataSO.ThrowingSpeed * Time.deltaTime;
            percent = currentTime / throwTime;

            Vector3 pos = Vector3.Lerp(start, end, percent);
            // _bobberTrm.rotation = Quaternion.LookRotation((Vector3.Lerp(start, end, percent + 0.1f) - pos).normalized);
            pos.y = start.y + (v0 * percent) +  (_gravity * percent * percent);
            _bobberTrm.position = pos;

            yield return null;
        }

        _controller.ActionData.IsUnderWater = true;
    }

    private void OnAnimationEndHandle(){
        _isThrowing = true;
        GameManager.Instance.GetManager<CameraManager>().SetVCam(CameraState.BOBBER_FOLLOW);
        StartCoroutine(ThrowTo());
    }
}
