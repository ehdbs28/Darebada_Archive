using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Core;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance = null;
    // ?´ê²ƒ???˜ì¤‘??ê²Œì„ë§¤ë‹ˆ?€?ì„œ ?¸ìŠ¤?´ìŠ¤ ê´€ë¦?

    private PlayerInput _playerInput;

    public event Action<float> OnMovementEvent = null;
    public event Action<float> OnRotationEvent = null;
    public event Action<bool> OnMouseClickEvent = null;

    private Vector3 _mousePosition;

    public Vector3 MousePosition => _mousePosition;
    public Vector3 MousePositionToGroundRayPostion{
        get{
            RaycastHit hit;
            bool isHit = Physics.Raycast(Define.MainCam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, _whatIsGround);
            return (isHit) ? hit.point : Vector3.zero;
        }
    }

    private LayerMask _whatIsGround;

    private void Awake() {
        if(Instance == null){
            Instance = this;
        }

        _playerInput = GetComponent<PlayerInput>();

        // ?˜ì¤‘??ë°”ê¾¸ê¸?
        _whatIsGround = LayerMask.GetMask("TestGroundLayer");
    }

    // new InputManager?ì„œ Event ?•ì‹?¼ë¡œ ?˜ê²¨???¤í–‰?˜ëŠ” ì¹œêµ¬?¤ì„

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
}
