using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class PlayableMovementModule : CommonModule<PlayerController>
{
    private Rigidbody _rigid;

    private Vector3 _dir;
    private Vector3 _movement;
    private Vector3 _movePivot;

    private bool _isMovement = false;

    private float _currentVelocity = 0f;

    public override void SetUp(Transform rootTrm)
    {
        base.SetUp(rootTrm);

        _rigid = rootTrm.GetComponent<Rigidbody>();

        GameManager.Instance.GetManager<InputManager>().OnMouseClickEvent += OnMouseClick;
    }

    public override void UpdateModule()
    {
        if(_isMovement){
            _dir = (GameManager.Instance.GetManager<InputManager>().MousePosition - _movePivot).normalized;
        }

        Movement();
    }

    public override void FixedUpdateModule()
    {
        _rigid.velocity = _movement * _currentVelocity;
    }

    private void Movement(){
        if(_isMovement){
            // if(Vector3.Dot(_dir, _movement) < 0){
            //     _currentVelocity = 0f;
            // }
            _movement = new Vector3(_dir.x, 0f, _dir.y);
        }
        _currentVelocity = CalcVelocity();
    }

    private float CalcVelocity(){
        if(_isMovement){
            _currentVelocity += _controller.DataSO.Acceleration * Time.deltaTime;
        }
        else{
            _currentVelocity -= _controller.DataSO.Deceleration * Time.deltaTime; 
        }

        return Mathf.Clamp(_currentVelocity, 0f, _controller.DataSO.MaxSpeed);
    }

    private void OnMouseClick(bool value){
        _isMovement = value;
        
        if(value)
            _movePivot = GameManager.Instance.GetManager<InputManager>().MousePosition;
    }
}
