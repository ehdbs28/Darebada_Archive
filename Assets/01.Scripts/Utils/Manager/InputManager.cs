using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Core;

public class InputManager : MonoBehaviour, IManager
{
    private PlayerInput _playerInput;

    public event Action<float> OnMovementEvent = null;
    public event Action<float> OnRotationEvent = null;
    public event Action<bool> OnMouseClickEvent = null;

    private Vector3 _mousePosition;

    public Vector3 MousePosition => _mousePosition;
    public Vector3 MousePositionToGroundRayPostion{
        get{
            RaycastHit hit;
            bool isHit = Physics.Raycast(Define.MainCam.ScreenPointToRay(Input.mousePosition), out hit, _whatIsGround);

            return (isHit) ? hit.point : Vector3.zero;
        }
    }

    private LayerMask _whatIsGround;

    public InputManager(){
        ResetManager();
    }

    public void InitManager() {
        _playerInput = GameManager.Instance.GetComponent<PlayerInput>();

        // ?òÏ§ë??Î∞îÍæ∏Í∏?
        _whatIsGround = LayerMask.GetMask("TestGroundLayer");
    }

    // new InputManager?êÏÑú Event ?ïÏãù?ºÎ°ú ?òÍ≤®???§Ìñâ?òÎäî ÏπúÍµ¨?§ÏûÑ

    public void OnMovement(InputValue value){
        OnMovementEvent?.Invoke(value.Get<float>());
    }

    public void OnRotation(InputValue value){
        OnRotationEvent?.Invoke(value.Get<float>());
    }

    public void OnMouseClick(InputValue value){
        OnMouseClickEvent?.Invoke(value.Get<float>() > 0);
    }

    public void OnMousePosition(InputValue value){
        _mousePosition = value.Get<Vector2>();
    }

    public void ResetManager(){}
    public void UpdateManager(){}
}
