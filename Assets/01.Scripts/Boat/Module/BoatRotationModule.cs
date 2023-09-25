using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatRotationModule : CommonModule<BoatController>
{
    private Rigidbody _rigid;

    private float _dir = 0f;
    private Vector3 _rotate = Vector3.zero;

    private float _rotationMaxSpeed => _controller.CurBoatData.RotationSpeed;
    private float _rotateAcceleration => _controller.CurBoatData.RotationAcceleration;
    private float _rotateDeceleration => _controller.CurBoatData.RotationDeceleration;

    private float _currentRotateVelocity;

    private float _startPos;
    private bool _isClick = false;

    private const float offset = 20f;

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
        GameManager.Instance.GetManager<InputManager>().OnTouchEvent += OnTouch;
        GameManager.Instance.GetManager<InputManager>().OnTouchUpEvent += OnTouchUp;
        GameManager.Instance.GetManager<InputManager>().OnTouchPosition += OnMousePos;
    }

    private void SetRotationValue(float value){
        _dir = value;
    }

    private void OnTouch()
    {
        _isClick = true;
        _startPos = GameManager.Instance.GetManager<InputManager>().TouchPosition.x;
    }

    private void OnTouchUp()
    {
        _isClick = false;
        SetRotationValue(0);
    }

    private void OnMousePos(Vector2 mousePos)
    {
        if (_isClick)
        {
            float cur = GameManager.Instance.GetManager<InputManager>().TouchPosition.x;

            if (Mathf.Abs(cur - _startPos) >= offset)
            {
                if (cur > _startPos)
                {
                    SetRotationValue(1);
                }
                else if (cur < _startPos)
                {
                    SetRotationValue(-1);
                }
            }
            else
            {
                SetRotationValue(0);
            }
        }
    }

    private void Rotate(){
        if(Mathf.Abs(_dir) > 0 && _controller.BoatActionData.IsMoveBoat){
            if(Vector3.Dot(_controller.transform.up * _dir, _rotate) < 0){
                _currentRotateVelocity = 0f;
            }
            _rotate = _controller.transform.up * _dir;
        }

        _currentRotateVelocity = CalcVelocity();
    }

    private float CalcVelocity(){
        if(Mathf.Abs(_dir) > 0 && _controller.BoatActionData.IsMoveBoat){
            _currentRotateVelocity += _rotateAcceleration * Time.deltaTime;
        }
        else{
            _currentRotateVelocity -= _rotateDeceleration * Time.deltaTime;
        }

        return Mathf.Clamp(_currentRotateVelocity, 0f, _rotationMaxSpeed);
    }
}
