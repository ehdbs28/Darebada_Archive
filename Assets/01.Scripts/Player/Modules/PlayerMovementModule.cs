using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using TMPro;
using UnityEngine.XR;

public class PlayerMovementModule : CommonModule<PlayerController>
{
    private CharacterController _characterController;

    private Vector3 _dir;
    private Vector3 _movement;
    private Vector3 _movePivot;

    private bool _moveStart;
    private bool _canMove;

    [SerializeField] 
    private float _minDistance;
    
    [SerializeField] 
    private float _maxDistance;

    private float _currentVelocity;
    private float _currentMaxVelocity;

    private JoyStickPopup _joyStick = null;
    
    public override void SetUp(Transform rootTrm)
    {
        base.SetUp(rootTrm);

        _characterController = rootTrm.GetComponent<CharacterController>();

        _canMove = false;
        _moveStart = false;
        _currentVelocity = 0f;

        GameManager.Instance.GetManager<InputManager>().OnTouchEvent += OnTouch;
        GameManager.Instance.GetManager<InputManager>().OnTouchUpEvent += OnTouchUp;
    }

    public override void UpdateModule()
    {
        if(_moveStart){
            Vector3 mousePos = GameManager.Instance.GetManager<InputManager>().TouchPosition;

            float distance = Vector3.Distance(_movePivot, mousePos);

            _canMove = distance >= _minDistance;

            if (_canMove)
            {
                _currentMaxVelocity = Mathf.Lerp(0f, _controller.DataSO.MaxSpeed, distance / _maxDistance);
            }
            else
            {
                _currentMaxVelocity = 0f;
            }
            
            _dir = (mousePos - _movePivot).normalized;
            
            if(_joyStick != null)
                _joyStick.SetDirPos(_dir * _currentMaxVelocity);
        }

        Movement();
    }

    public override void FixedUpdateModule()
    {
        _characterController.Move(_movement * (_currentVelocity * Time.deltaTime));
    }

    private void Movement(){
        if(_moveStart && _canMove)
        {
            float angle = Mathf.Atan2(_dir.y * Mathf.Deg2Rad, _dir.x * Mathf.Deg2Rad);
            angle += Define.MainCam.transform.rotation.y;
            angle -= 90 * Mathf.Deg2Rad;
            
            transform.parent.eulerAngles = new Vector3(0, -angle * Mathf.Rad2Deg + 90, 0);
            
            _dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
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
        if(_moveStart){
            _currentVelocity += _controller.DataSO.Acceleration * Time.deltaTime;
        }
        else{
            _currentVelocity -= _controller.DataSO.Deceleration * Time.deltaTime; 
        }

        return Mathf.Clamp(_currentVelocity, 0f, _currentMaxVelocity);
    }
    
    private void OnTouch()
    {
        _moveStart = true;
        _movePivot = GameManager.Instance.GetManager<InputManager>().TouchPosition;
        _joyStick = (JoyStickPopup)GameManager.Instance.GetManager<UIManager>().ShowPanel(UGUIType.JoyStick);
        _joyStick.SetInitPos(_movePivot);
    }

    private void OnTouchUp()
    {
        _moveStart = false;
        if (_joyStick != null)
        {
            _joyStick.RemoveRoot();
            _joyStick = null;
        }
    }
}
