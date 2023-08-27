using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Core;

public class JoyStickPopup : UGUIPopup
{
    [SerializeField]
    private RectTransform _inValueRectTrm;
    
    [SerializeField]
    private float _offset;

    public override void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true)
    {
        base.SetUp(document, clearScreen, blur, timeStop);
        SetDirPos(Vector3.zero);
    }

    public override void FindElement()
    {
    }

    public override void AddEvent()
    {
    }

    public override void RemoveEvent()
    {
    }

    public void SetDirPos(Vector3 pos)
    {
        _inValueRectTrm.anchoredPosition = pos * _offset;
    }

    public void SetInitPos(Vector2 screenPos)
    {
        Vector3 touchPos = GameManager.Instance.GetManager<InputManager>().TouchPosition;
        _uiObject.GetComponent<RectTransform>().anchoredPosition = touchPos;
    }
}
