using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingChargingState : FishingState
{
    [SerializeField]
    private Transform _bobberPos;

    private Transform _bobberTrm;
    private Transform _playerTrm;

    private float _maxChargingPower => _controller.DataSO.MaxChargingPower;
    private float _currentChargingPower = 0f;
    private float _powerDir = 1f;

    private Vector3 _currentDir;
    
    [SerializeField]
    private float _minChargingPower;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);

        _bobberTrm = agentRoot.Find("Bobber");
        _playerTrm = agentRoot;
    }

    public override void EnterState()
    {
        GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.Casting, true, false, false);

        _currentChargingPower = 0f;
        _powerDir = 1f;
    }

    public override void ExitState()
    {
        GameManager.Instance.GetManager<UIManager>().GetPanel(PopupType.Casting).RemoveRoot();
        
        _controller.ActionData.LastChargingPower = _currentChargingPower + _minChargingPower;
        _controller.ActionData.LastThrowDirection = _currentDir;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _bobberTrm.position = _bobberPos.position;

        _currentChargingPower += _powerDir * _controller.DataSO.ChargingSpeed * Time.deltaTime;

        if(_currentChargingPower >= _controller.DataSO.MaxChargingPower || _currentChargingPower <= 0f)
            _powerDir *= -1;

        (GameManager.Instance.GetManager<UIManager>().GetPanel(PopupType.Casting) as CastingPopup).SetValue(_currentChargingPower / _maxChargingPower);

        Vector3 mousePos = GameManager.Instance.GetManager<InputManager>().GetMouseRayPoint();
        mousePos.y = _playerTrm.position.y;
        _currentDir = mousePos - _playerTrm.position;
        Rotation(_currentDir);
    }

    private void Rotation(Vector3 target){
        Vector3 currentFrontVec = _playerTrm.transform.forward;
        float angle = Vector3.Angle(currentFrontVec, target);

        if(angle >= 10f){
            Vector3 result = Vector3.Cross(currentFrontVec, target);

            float sign = result.y > 0 ? 1 : -1;
            _playerTrm.rotation = Quaternion.Euler(0, sign * _controller.DataSO.RotationSpeed * Time.deltaTime, 0) * _playerTrm.rotation;
        }
        else{
            // 다 돌아갔을 때 할 일인데 아직 없음
        }
    }   

    private void SetThrowingHandle(){
        _controller.ActionData.IsThrowing = true;
    }
}
