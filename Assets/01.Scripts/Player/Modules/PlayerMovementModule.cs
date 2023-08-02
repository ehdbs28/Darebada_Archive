using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using UnityEngine.XR;

public class PlayerMovementModule : CommonModule<PlayerController>
{
    private CharacterController _characterController;

    private Vector3 _dir;
    private Vector3 _movement;
    private Vector3 _movePivot;

    private bool _isMovement = false;
    private float _currentVelocity = 0f;

    public override void SetUp(Transform rootTrm)
    {
        base.SetUp(rootTrm);

        _characterController = rootTrm.GetComponent<CharacterController>();
        _isMovement = false;
        _currentVelocity = 0f;

        GameManager.Instance.GetManager<InputManager>().OnMouseClickEvent += OnMouseClick;
    }

    public override void UpdateModule()
    {
        if(_isMovement){
            Vector3 mousePos = GameManager.Instance.GetManager<InputManager>().MousePosition;
            float distance = Vector3.Distance(_movePivot, mousePos);
            Debug.Log(distance);
            if (distance >= 100)
                _dir = (mousePos - _movePivot).normalized;
            else
                _dir = Vector3.zero;
        }

        Movement();
    }

    public override void FixedUpdateModule()
    {
        _characterController.Move(_movement * (_currentVelocity * Time.deltaTime));
    }

    private void Movement(){
        if(_isMovement)
        {
            float angle = Mathf.Atan2(_dir.y * Mathf.Deg2Rad, _dir.x * Mathf.Deg2Rad);
            angle += Define.MainCam.transform.rotation.y;
            angle -= 90 * Mathf.Deg2Rad;
            _dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
            transform.parent.eulerAngles = new Vector3(0, -angle * Mathf.Rad2Deg + 90, 0);
            _movement = new Vector3(_dir.x, 0, _dir.y);
            _movement.Normalize();
            _currentVelocity = CalcVelocity();
            
            _controller.GetModule<PlayerAnimatorModule>().RunningToggle(true);
        }
        else
        {
            _movement = Vector3.zero;
            _currentVelocity = 0f;
            
            _controller.GetModule<PlayerAnimatorModule>().RunningToggle(false);
        }
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

        if (value)
        {
            _movePivot = GameManager.Instance.GetManager<InputManager>().MousePosition;
        }
    }
}
