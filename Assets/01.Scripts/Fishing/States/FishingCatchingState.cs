using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingCatchingState : FishingState
{
    private Transform _bobberTrm;

    private bool _isReadyToCatch;

    private Vector3 _start;
    private Vector3 _end;

    private float _stringLength => _controller.DataSO.StringLength;
    private float _lenght;

    private float _throwTime;
    private float _currentTime = 0f;
    private float _percent = 0f;
    private float percent {
        get {
            return _percent;
        }
        set{
            _percent = value;
            (GameManager.Instance.GetManager<UIManager>().GetPanel(ScreenType.Fishing) as FishingScreen).SetHeight(_percent, _percent * _lenght);
        }
    }

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);

        _bobberTrm = agentRoot.Find("Bobber");
    }

    public override void EnterState()
    {
        GameManager.Instance.GetManager<UIManager>().ShowPanel(ScreenType.Fishing);

        _start = _bobberTrm.position;
        _end = _start + _controller.ActionData.LastThrowDirection.normalized; 

        _lenght = _stringLength - _controller.ActionData.LastChargingPower * 10 / _controller.DataSO.MaxChargingPower + 1; 
        
        _end.y = -_lenght;

        _throwTime = Mathf.Max(0.3f, Vector3.Distance(_start, _end)) / _controller.DataSO.ThrowingSpeed;
        _currentTime = 0f;
        percent = 0f;

        _isReadyToCatch = false;

        StartCoroutine(ToThrow());
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        if(_isReadyToCatch){
            // 나중에 조건 고치기
            if(Input.GetKey(KeyCode.Space)){
                percent -= _controller.DataSO.ThrowingSpeed * Time.deltaTime / _throwTime;
                _bobberTrm.position = GetLerpPos();

                if(percent <= 0){
                    _controller.ActionData.IsFishing = false;
                    _controller.ActionData.IsUnderWater = false;
                }
            }
        }
        else{
            // 나중에 조건 고치기
            if(Input.GetKeyDown(KeyCode.Space)){
                percent = 1f;
                _isReadyToCatch = true;
                _controller.Bait.StartCheck = true;
            }
        }

        base.UpdateState();
    }

    private IEnumerator ToThrow(){
        while(percent < 1 && _isReadyToCatch == false){
            _currentTime += _controller.DataSO.ThrowingSpeed * Time.deltaTime;
            percent = _currentTime / _throwTime;

            _bobberTrm.position = GetLerpPos();

            yield return null;
        }

        percent = 1f;
        _bobberTrm.position = GetLerpPos();

        _isReadyToCatch = true;
        _controller.Bait.StartCheck = true;

        //_start = new Vector3(_controller.ActionData.InitPosition.x, 0, _controller.ActionData.InitPosition.z);
    }

    private Vector3 GetLerpPos(){
        return Vector3.Lerp(_start, _end, percent);
    }
}
