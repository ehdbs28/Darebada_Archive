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

    public event Action OnTutorialClick = null;

    private Vector2 _touchPosition;
    public Vector2 TouchPosition => _touchPosition;

    [SerializeField]
    private bool _canInitSetting = false;

    public void Awake()
    {
        if (_canInitSetting)
        {
            Application.targetFrameRate = 60;
            InitManager();
        }

    }

    public void InitManager()
    {
        _inputAction = new GameInputControls();
        
        EnableInputAction(true);
        _inputAction.Touch.Touch.performed += TouchHandle;
        _inputAction.Touch.Touch.canceled += TouchUpHandle;
        _inputAction.Touch.TouchPosition.performed += TouchPositionHandle;

        _inputAction.Mouse.Enable();
        _inputAction.Mouse.Click.performed += MouseClickHandle;
        _inputAction.Mouse.Click.canceled += MouseUpHandle;
        _inputAction.Mouse.MousePos.performed += MousePosHandle;

        
    }

    #region MOBILE INPUT

    private void TouchHandle(InputAction.CallbackContext context)
    {
        if(GameManager.Instance != null && GameManager.Instance.GetManager<UIManager>().OnElement(_touchPosition))
            return;

        if (GameManager.Instance != null && GameManager.Instance.GetManager<TutorialManager>().InTut)
        {
            OnTutorialClick?.Invoke();
            return;
        }
        OnTouchEvent?.Invoke();
    }
    
    private void TouchUpHandle(InputAction.CallbackContext context)
    {
        if (GameManager.Instance != null && GameManager.Instance.GetManager<TutorialManager>().InTut)
            return;
        
        OnTouchUpEvent?.Invoke();
    }

    private void TouchPositionHandle(InputAction.CallbackContext context)
    {
        _touchPosition = Touchscreen.current.position.ReadValue();
        OnTouchPosition?.Invoke(_touchPosition);
    }
    
    #endregion

    #region PC INPUT

    private void MouseClickHandle(InputAction.CallbackContext context)
    {
        if (GameManager.Instance != null && GameManager.Instance.GetManager<UIManager>().OnElement(_touchPosition))
        {
            return;
        }

        if (GameManager.Instance != null && GameManager.Instance.GetManager<TutorialManager>().InTut)
        {
            OnTutorialClick?.Invoke();
            return;
        }

        OnTouchEvent?.Invoke();
    }
    
    private void MouseUpHandle(InputAction.CallbackContext context)
    {
        if (GameManager.Instance != null && GameManager.Instance.GetManager<TutorialManager>().InTut)
            return;
        
        OnTouchUpEvent?.Invoke();
    }

    private void MousePosHandle(InputAction.CallbackContext context)
    {
        _touchPosition = Mouse.current.position.ReadValue();
        OnTouchPosition?.Invoke(_touchPosition);
    }
    
    # endregion
    
    public void EnableInputAction(bool enable)
    {
        if(enable)
            _inputAction.Touch.Enable();
        else
            _inputAction.Touch.Disable();
    }

    public Vector3 GetMouseRayPoint(out RaycastHit hitInfo, string layerName = "Ground"){
        Ray ray = Define.MainCam.ScreenPointToRay(_touchPosition);
        RaycastHit hit;
        bool isHit = Physics.Raycast(ray, out hit,Mathf.Infinity, LayerMask.GetMask(layerName));
        hitInfo = hit;
        return (isHit) ? hit.point : Vector3.zero;
    }

    public void ResetManager(){}
    public void UpdateManager(){}
}
