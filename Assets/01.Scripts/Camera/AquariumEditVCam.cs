using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquariumEditVCam : VCam
{
    private Vector2 _lastTouchPos;

    private bool _isMovement;
    
    [SerializeField]
    private float _distroyZoneOffset = 20;
    
    [SerializeField]
    private float _camMoveSpeed = 10f;

    public override void SelectVCam()
    {
        base.SelectVCam();
        _isMovement = false;
        GameManager.Instance.GetManager<InputManager>().OnTouchEvent += OnTouch;
        GameManager.Instance.GetManager<InputManager>().OnTouchUpEvent += OnTouchUp;
    }

    public override void UnselectVCam()
    {
        base.UnselectVCam();
        _isMovement = false;
        GameManager.Instance.GetManager<InputManager>().OnTouchEvent -= OnTouch;
        GameManager.Instance.GetManager<InputManager>().OnTouchUpEvent -= OnTouchUp;
    }

    public override void UpdateCam()
    {
        if (_isMovement)
        {
            var curTouchPos = GameManager.Instance.GetManager<InputManager>().TouchPosition;
            var distance = Vector2.Distance(_lastTouchPos, curTouchPos);

            if (distance >= _distroyZoneOffset)
            {
                var dir = (curTouchPos - _lastTouchPos).normalized;
                var movement =  Quaternion.Euler(0, 45, 0) * -new Vector3(dir.x, 0, dir.y) * (_camMoveSpeed * Time.deltaTime);
                transform.position += movement;
                _lastTouchPos = curTouchPos;
            }
        }
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    private void OnTouch()
    {
        if (AquariumManager.Instance.state == AquariumManager.STATE.CAMMOVE)
        {
            if (!_isMovement)
            {
                _isMovement = true;
                _lastTouchPos = GameManager.Instance.GetManager<InputManager>().TouchPosition;
            }
        }
    }

    private void OnTouchUp()
    {
        if (_isMovement)
        {
            _isMovement = false;
        }
    }
}
