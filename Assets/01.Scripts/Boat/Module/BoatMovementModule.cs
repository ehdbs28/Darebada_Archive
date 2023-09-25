using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BoatMovementModule : CommonModule<BoatController>
{
    private Rigidbody _rigid;
    private FishingController _fishingController;

    private Vector3 _movement = Vector3.zero;
    private float _inputDir = 0f;

    private float _moveMaxSpeed => _controller.CurBoatData.MaxSpeed;
    private float _forwardAcceleration => _controller.CurBoatData.ForwardAcceleration;
    private float _backwardAcceleration => _controller.CurBoatData.BackwardAcceleration;
    private float _deceleration => _controller.CurBoatData.Deceleration;

    private float _currentVelocity = 0f;

    public override void SetUp(Transform rootTrm)
    {
        base.SetUp(rootTrm);

        _rigid = rootTrm.GetComponent<Rigidbody>();
        _fishingController = GameObject.Find("Player").GetComponentInChildren<FishingController>();

        StopImmediately();
    }

    public override void UpdateModule()
    {
        _controller.BoatActionData.IsMoveBoat = _currentVelocity > 0;

        if(_fishingController.ActionData.IsFishing)
            return;

        Movement();
    }

    public override void FixedUpdateModule()
    {
        _rigid.velocity = _movement * _currentVelocity;
        Vector3 trPos =_rigid.transform.position;
        trPos.x = Mathf.Clamp(trPos.x, -180, 180);
        trPos.z = Mathf.Clamp(trPos.z, -180, 180);
        _rigid.transform.position = trPos;
    }

    public void SetMovementValue(float value){
        _inputDir = value;
    }

    private void Movement(){
        if(Mathf.Abs(_inputDir) > 0){
            if(Vector3.Dot(_controller.transform.forward * _inputDir, _movement) < 0){
                _currentVelocity = 0f;
            }
            _movement = _controller.transform.forward * _inputDir;
        }
        _currentVelocity = CalcVelocity();
    }

    private float CalcVelocity(){
        if(Mathf.Abs(_inputDir) > 0){
            if(_inputDir > 0){
                _currentVelocity += _forwardAcceleration * Time.deltaTime;
            }
            else{
                _currentVelocity += _backwardAcceleration * Time.deltaTime;
            }
        }
        else{
            _currentVelocity -= _deceleration * Time.deltaTime; 
        }

        return Mathf.Clamp(_currentVelocity, 0f, _moveMaxSpeed);
    }

    private void StopImmediately(){
        _currentVelocity = 0;
        _rigid.velocity = Vector3.zero;
    }
}