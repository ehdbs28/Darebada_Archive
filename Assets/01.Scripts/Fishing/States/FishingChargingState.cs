using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingChargingState : FishingState
{
    private Transform _playerTrm;

    private float _maxChargingPower => (GameManager.Instance.GetManager<DataManager>().GetData(DataType.FishingData) as FishingData).MaxChargingPower;
    private float _currentChargingPower = 0f;
    private float _powerDir = 1f;

    private Vector3 _currentDir;

    public override void EnterState()
    {
        GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.Casting, true, false, false);

        _playerTrm = _controller.transform.parent;

        _currentChargingPower = 0f;
        _powerDir = 1f;
    }

    public override void ExitState()
    {
        GameManager.Instance.GetManager<UIManager>().GetPanel(PopupType.Casting).RemoveRoot();
        
        _controller.ActionData.LastChargingPower = _currentChargingPower;
        _controller.ActionData.LastThrowDirection = _currentDir;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _currentChargingPower += _powerDir * _controller.FishingData.ChargingSpeed * Time.deltaTime;

        if(_currentChargingPower >= _controller.FishingData.MaxChargingPower || _currentChargingPower <= 0f)
            _powerDir *= -1;

        (GameManager.Instance.GetManager<UIManager>().GetPanel(PopupType.Casting) as CastingPopup).SetValue(_currentChargingPower / _maxChargingPower);

        _currentDir = GameManager.Instance.GetManager<InputManager>().MousePositionToGroundRayPostion - _playerTrm.position;
        _currentDir.y = _playerTrm.position.y;
        Rotation(_currentDir);
    }

    private void Rotation(Vector3 target){
        Vector3 currentFrontVec = _playerTrm.transform.forward;
        float angle = Vector3.Angle(currentFrontVec, target);

        if(angle >= 10f){
            Vector3 result = Vector3.Cross(currentFrontVec, target);

            float sign = result.y > 0 ? 1 : -1;
            _playerTrm.rotation = Quaternion.Euler(0, sign * _controller.FishingData.RotationSpeed * Time.deltaTime, 0) * _playerTrm.rotation;
        }
        else{
            // 다 돌아갔을 때 할 일인데 아직 없음
        }
    }   
}
