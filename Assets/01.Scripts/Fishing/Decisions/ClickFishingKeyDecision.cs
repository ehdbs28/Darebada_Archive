using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFishingKeyDecision : FishingDecision
{
    private  bool _result = false;
    private bool _prevResult = false;

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
        if (_prevResult != _result)
            _prevResult = _result;

        if((_result && !IsReverse) || (!_result && IsReverse))
        {
            _result = false;
            return _prevResult;
        }
        else
        {
            return _result;
        }
    }

    private void OnTouch(){
        if(_prevResult == _result)
            _result = true;
    }

    private void OnTouchUp()
    {
        if(_prevResult == _result)
            _result = false;
    }
}
