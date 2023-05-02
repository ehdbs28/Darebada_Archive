using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovementModule : CommonModule
{
    private Rigidbody _rigid;
    private FishingController _fishingController;

    private Vector3 _movement = Vector3.zero;
    private float _inputDir = 0f;

    private float _moveMaxSpeed => _controller.BoatData.BoatMaxSpeed;
    private float _forwardAcceleration => _controller.BoatData.BoatForwardAcceleration;
    private float _backwardAcceleration => _controller.BoatData.BoatBackwardAcceleration;
    private float _deceleration => _controller.BoatData.BoatDeceleration;

    private float _currentVelocity = 0f;

    public override void SetUp(Transform rootTrm)
    {
        base.SetUp(rootTrm);

        _rigid = rootTrm.GetComponent<Rigidbody>();
        _fishingController = GameObject.Find("TestPlayer").GetComponentInChildren<FishingController>();

        AddEvent();
        StopImmediately();
    }

    public override void UpdateModule()
    {
        if(_fishingController.ActionData.IsFishing)
            return;

        Movement();
    }

    public override void FixedUpdateModule()
    {
        _rigid.velocity = _movement * _currentVelocity;
    }

    private void AddEvent(){
        InputManager.Instance.OnMovementEvent += SetMovementValue;
    }

    private void SetMovementValue(float value){
        _inputDir = value;
    }

    private void Movement(){
        if(Mathf.Abs(_inputDir) > 0){
            if(Vector3.Dot(_controller.transform.forward * _inputDir, _movement) < 0){
                _currentVelocity = 0f;
            }
            _controller.BoatData.IsMoveBoat = true;
            _movement = _controller.transform.forward * _inputDir;
        }
        else{
            _controller.BoatData.IsMoveBoat = false;
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