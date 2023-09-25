using Core;
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

    private float _maxChargingPower
    {
        get
        {
            var sheetdata = (FishingUpgradeTable)GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.FishingUpgradeData);
            var data = (FishingData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.FishingData);

            return sheetdata.DataTable[0].Value[data.ThrowPower_Level-1];
        }
    }
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

        if(_currentChargingPower >= _maxChargingPower || _currentChargingPower <= 0f)
            _powerDir *= -1;

        (GameManager.Instance.GetManager<UIManager>().GetPanel(PopupType.Casting) as CastingPopup).SetValue(_currentChargingPower / _maxChargingPower);

        //Vector3 mousePos = GameManager.Instance.GetManager<InputManager>().GetMouseRayPoint();
        //mousePos.y = _playerTrm.position.y;
        //_currentDir = mousePos - _playerTrm.position;

        Vector3 cameraForward = Camera.main.transform.forward;
        _currentDir = cameraForward;
        Rotation(_currentDir);
    }

    private void Rotation(Vector3 target)
    {
        Vector3 currentFrontVec = _playerTrm.transform.forward;
        /*Vector3 currentFrontVec = _playerTrm.transform.forward;
        float angle = Vector3.Angle(currentFrontVec, target);

        if(angle >= 10f){
        }
        else{
            // ???�아갔을 ?????�인???�직 ?�음
        }*/
        if (Define.MainCam.transform != null)
        {
            // Calculate the desired rotation for the object based on the camera's rotation.
            Quaternion targetRotation = Quaternion.Euler(0f, Define.MainCam.transform.eulerAngles.y, 0f);

            // Apply the target rotation and offset to the object's position.
            _playerTrm.rotation = targetRotation;
            //_playerTrm.position = Define.MainCam.transform.position + offset;
        }
    }

    private void SetThrowingHandle(){
        _controller.ActionData.IsThrowing = true;
    }
}
