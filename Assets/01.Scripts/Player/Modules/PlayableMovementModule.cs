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

    [SerializeField] private PlayerAnimator _animator;

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
            Vector3 mousePos = GameManager.Instance.GetManager<InputManager>().MousePosition;
            if (Vector3.Distance(_movePivot, mousePos) >= 0.5f)
                _dir = GameManager.Instance.GetManager<InputManager>().MousePosition - _movePivot;
            else
                _dir = Vector3.zero;
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
                float angle = Mathf.Atan2(_dir.y * Mathf.Deg2Rad, _dir.x * Mathf.Deg2Rad);
                angle += Define.MainCam.transform.rotation.y;
                angle -= 90 * Mathf.Deg2Rad;
                _dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
                transform.parent.eulerAngles = new Vector3(0, -angle * Mathf.Rad2Deg + 90, 0);
                _movement = new Vector3(_dir.x, 0f, _dir.y);
                _movement.y = 0;
                _movement.Normalize();

            _animator.SetBool("IsRunning", true);
        }else 
            _animator.SetBool("IsRunning", false);
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
