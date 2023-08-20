using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFishingKeyDecision : FishingDecision
{
    bool _result = false;

    public void AddEvent()
    {
        GameManager.Instance.GetManager<InputManager>().OnMouseClickEvent += OnMouseClick;
    }

    public void RemoveEvent()
    {
        GameManager.Instance.GetManager<InputManager>().OnMouseClickEvent -= OnMouseClick;
    }

    public override bool MakeADecision()
    {
        return _result;
    }

    private void OnMouseClick(bool value){
        _result = value;
    }
}
