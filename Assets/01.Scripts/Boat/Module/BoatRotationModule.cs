using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatRotationModule : CommonModule
{
    private Rigidbody _rigid;

    private float _dir = 0f;
    private Vector3 _rotate = Vector3.zero;

    private float _rotationMaxSpeed => _controller.BoatData.BoatMaxRotationSpeed;
    private float _rotateAcceleration => _controller.BoatData.BoatRotationAcceleration;
    private float _rotateDeceleration => _controller.BoatData.BoatRotationDeceleration;

    private float _currentRotateVelocity;

    public override void SetUp(Transform rootTrm)
    {
        base.SetUp(rootTrm);

        _rigid = rootTrm.GetComponent<Rigidbody>();

        AddEvent();
    }

    public override void UpdateModule()
    {
        Rotate();
    }

    public override void FixedUpdateModule()
    {
        _rigid.angularVelocity = _rotate * _currentRotateVelocity;
    }

    private void AddEvent(){
        _controller.GetModule<BoatInputModule>().OnRotateKeyPress += SetRotationValue;
    }

    private void SetRotationValue(float value){
        _dir = value;
    }

    private void Rotate(){
        if(Mathf.Abs(_dir) > 0 && _controller.BoatData.IsMoveBoat){
            if(Vector3.Dot(_controller.transform.up * _dir, _rotate) < 0){
                _currentRotateVelocity = 0f;
            }
            _rotate = _controller.transform.up * _dir;
        }

        _currentRotateVelocity = CalcVelocity();
    }

    private float CalcVelocity(){
        if(Mathf.Abs(_dir) > 0 && _controller.BoatData.IsMoveBoat){
            _currentRotateVelocity += _rotateAcceleration * Time.deltaTime;
        }
        else{
            _currentRotateVelocity -= _rotateDeceleration * Time.deltaTime;
        }

        return Mathf.Clamp(_currentRotateVelocity, 0f, _rotationMaxSpeed);
    }
}
