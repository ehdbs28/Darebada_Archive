using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingChargingState : FishingState
{
    [SerializeField] private LayerMask _whatIsGround;
    private Camera _mainCam;
    private Transform _playerTrm;

    private float _currentChargingPower = 0f;
    private float _powerDir = 1f;

    private Vector3 _currentDir;

    public override void EnterState()
    {
        _mainCam = Camera.main;
        _playerTrm = _controller.transform.parent;
        _actionData.IsFishing = true;

        _currentChargingPower = -_actionData.MaxChargingPower;
        _powerDir = 1f;
    }

    public override void ExitState()
    {
        _actionData.LastChargingPower = _currentChargingPower;
        _actionData.LastThrowDirection = _currentDir;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _currentChargingPower += _powerDir * _actionData.ChargingSpeed * Time.deltaTime;

        if(Mathf.Abs(_currentChargingPower) >= _actionData.MaxChargingPower)
            _powerDir *= -1;

        Debug.Log(_currentChargingPower);

        _currentDir = GetMousePos() - _playerTrm.position;
        _currentDir.y = _playerTrm.position.y;
        Rotation(_currentDir);
    }

    private void Rotation(Vector3 target){
        Vector3 currentFrontVec = _playerTrm.transform.forward;
        float angle = Vector3.Angle(currentFrontVec, target);

        if(angle >= 10f){
            Vector3 result = Vector3.Cross(currentFrontVec, target);

            float sign = result.y > 0 ? 1 : -1;
            _playerTrm.rotation = Quaternion.Euler(0, sign * _actionData.RotationSpeed * Time.deltaTime, 0) * _playerTrm.rotation;
        }
        else{
            // 다 돌아갔을 때 할 일인데 아직 없음
        }
    }   

    // 이것도 인풋 매니저로 빼자
    private Vector3 GetMousePos(){
        RaycastHit hit;
        bool isHit = Physics.Raycast(_mainCam.ScreenPointToRay(Input.mousePosition), out hit, _whatIsGround);

        return (isHit) ? hit.point : _playerTrm.position;
    }
}
