using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovementModule : CommonModule
{
    private Rigidbody _rigid;

    private Vector3 _movement = Vector3.zero;
    private Vector3 _inputDir = Vector3.zero;

    private float _moveMaxSpeed => _controller.BoatData.BoatMaxSpeed;
    private float _acceleration => _controller.BoatData.BoatAcceleration;
    private float _deceleration => _controller.BoatData.BoatDeceleration;

    private float _currentVelocity = 0f;

    public override void SetUp(Transform rootTrm)
    {
        base.SetUp(rootTrm);

        _rigid = rootTrm.GetComponent<Rigidbody>();

        AddEvent();
        StopImmediately();
    }

    public override void UpdateModule()
    {
        Movement();
    }

    public override void FixedUpdateModule()
    {
        _rigid.velocity = _movement * _currentVelocity;
    }

    private void AddEvent(){
        _controller.GetModule<BoatInputModule>().OnMovementKeyPress += SetMovementVelocity;
    }

    private void SetMovementVelocity(Vector3 value){
        _inputDir = value;
    }

    private void Movement(){
        if(_inputDir.sqrMagnitude > 0){
            if(Vector3.Dot(_inputDir, _movement) < 0){
                _currentVelocity = 0f;
            }
            _movement = _inputDir;
        }
        _currentVelocity = CalcVelocity();
    }

    private float CalcVelocity(){
        if(_inputDir.sqrMagnitude > 0){
            _currentVelocity += _acceleration * Time.deltaTime;
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
