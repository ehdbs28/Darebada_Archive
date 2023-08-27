using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Core;

public class InputManager : MonoBehaviour, IManager
{
    private GameInputControls _inputAction;

    public event Action OnTouchEvent = null;
    public event Action OnTouchUpEvent = null;
    public event Action<Vector2> OnTouchPosition = null;
    
    [SerializeField]
    private RectTransform _canvas;

    private Vector2 _touchPosition;
    public Vector2 TouchPosition => _touchPosition;

    public void InitManager()
    {
        _inputAction = new GameInputControls();
        
        EnableInputAction(true);
        _inputAction.Touch.Touch.performed += TouchHandle;
        _inputAction.Touch.Touch.canceled += TouchUpHandle;
        _inputAction.Touch.TouchPosition.performed += TouchPositionHandle;
    }

    private void TouchHandle(InputAction.CallbackContext context)
    {
        if(GameManager.Instance.GetManager<UIManager>().OnElement(_touchPosition))
            return;

        OnTouchEvent?.Invoke();
    }

    private void TouchUpHandle(InputAction.CallbackContext context)
    {
        OnTouchUpEvent?.Invoke();
    }

    private void TouchPositionHandle(InputAction.CallbackContext context)
    {
        _touchPosition = Touchscreen.current.position.ReadValue();
        OnTouchPosition?.Invoke(_touchPosition);
    }

    public void EnableInputAction(bool enable)
    {
        if(enable)
            _inputAction.Touch.Enable();
        else
            _inputAction.Touch.Disable();
    }

    public Vector3 GetMouseRayPoint(string layerName = "Ground"){
        Ray ray = Define.MainCam.ScreenPointToRay(_touchPosition);
        RaycastHit hit;
        bool isHit = Physics.Raycast(ray, out hit,Mathf.Infinity, LayerMask.GetMask(layerName));
        return (isHit) ? hit.point : Vector3.zero;
    }

    public Vector2 ScreenToCanvasPos(Vector2 touchPos)
    {
        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas, touchPos, Define.MainCam, out canvasPos);
        return canvasPos;
    }

    public void ResetManager(){}
    public void UpdateManager(){}
}
