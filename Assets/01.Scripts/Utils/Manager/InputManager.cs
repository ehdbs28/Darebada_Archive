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

    public InputManager(){
        ResetManager();
    }

    public void InitManager() {

    }

    public void OnMovement(InputValue value){
        OnMovementEvent?.Invoke(value.Get<float>());
    }

    public void OnRotation(InputValue value){
        OnRotationEvent?.Invoke(value.Get<float>());
    }

    public void OnMouseClick(InputValue value){
        Debug.Log(GameManager.Instance.GetManager<UIManager>().OnElement(_mousePosition));
        if(GameManager.Instance.GetManager<UIManager>().OnElement(_mousePosition))
            return;

        OnMouseClickEvent?.Invoke(value.Get<float>() > 0);
    }

    public void OnMousePosition(InputValue value){
        Debug.Log(_mousePosition);
        _mousePosition = value.Get<Vector2>();
    }

    public Vector3 GetMouseRayPoint(string layerName = "Ground"){
        Ray ray = Define.MainCam.ScreenPointToRay(_mousePosition);
        RaycastHit hit;
        bool isHit = Physics.Raycast(ray, out hit,Mathf.Infinity, LayerMask.GetMask(layerName));

        return (isHit) ? hit.point : Vector3.zero;
    }

    public void ResetManager(){}
    public void UpdateManager(){}
}
