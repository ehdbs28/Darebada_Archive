using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoatControllerUI
{
    private VisualElement _handle;
    private BoatController _controller;

    private bool _isClick = false;

    private float _offset;
    private float _startPos;
    private float _delta;
    
    private int _dir;
    private int _curDir;

    public BoatControllerUI(VisualElement root, BoatController controller, float offset)
    {
        _dir = 0;
        _handle = root.Q("handle");
        _controller = controller;
        _offset = offset;
        AddEvent();
    }

    private void AddEvent()
    {
        _handle.RegisterCallback<MouseDownEvent>(OnMouseDown);
        GameManager.Instance.GetManager<InputManager>().OnTouchPosition += OnMouseMove;
        GameManager.Instance.GetManager<InputManager>().OnTouchUpEvent += OnMouseUp;
    }

    public void RemoveEvent()
    {
        _handle.UnregisterCallback<MouseDownEvent>(OnMouseDown);
        GameManager.Instance.GetManager<InputManager>().OnTouchPosition -= OnMouseMove;
        GameManager.Instance.GetManager<InputManager>().OnTouchUpEvent -= OnMouseUp;
    }

    private void OnMouseDown(MouseDownEvent e)
    {
        if (e.button == 0)
        {
            _isClick = true;
            _startPos = GameManager.Instance.GetManager<InputManager>().TouchPosition.y;
        }
    }

    private void OnMouseMove(Vector2 mousePos)
    {
        if (!_isClick)
            return;

        Debug.Log(1);

        float cur = mousePos.y;

        if (cur < _startPos)
        {
            _delta = _startPos - cur;
            _curDir = -1;
        }
        else if(cur > _startPos)
        {
            _delta = cur - _startPos;
            _curDir = 1;
        }
    }

    private void OnMouseUp()
    {
        if (!_isClick)
            return;

        if (_delta >= _offset)
        {
            _dir = Mathf.Clamp(_dir + _curDir, -1, 1);
            _handle.style.bottom = new StyleLength(new Length((_dir + 1) * 47, LengthUnit.Percent));
            _controller.GetModule<BoatMovementModule>().SetMovementValue(_dir);
        }
        
        _isClick = false;
    }
}
