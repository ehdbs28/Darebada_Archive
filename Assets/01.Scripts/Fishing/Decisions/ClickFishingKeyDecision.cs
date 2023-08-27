using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFishingKeyDecision : FishingDecision
{
    bool _result = false;

    public void AddEvent()
    {
        GameManager.Instance.GetManager<InputManager>().OnTouchEvent += OnTouch;
        GameManager.Instance.GetManager<InputManager>().OnTouchUpEvent += OnTouchUp;
    }

    public void RemoveEvent()
    {
        GameManager.Instance.GetManager<InputManager>().OnTouchEvent -= OnTouch;
        GameManager.Instance.GetManager<InputManager>().OnTouchUpEvent -= OnTouchUp;
    }

    public override bool MakeADecision()
    {
        return _result;
    }

    private void OnTouch(){
        _result = true;
    }

    private void OnTouchUp()
    {
        _result = false;
    }
}
