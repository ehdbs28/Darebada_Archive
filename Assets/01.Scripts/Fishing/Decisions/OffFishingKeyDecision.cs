using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffFishingKeyDecision : FishingDecision
{
    bool _result = false;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);

        GameManager.Instance.GetManager<InputManager>().OnMouseClickEvent += OnMouseClick;
    }

    public override bool MakeADecision()
    {
        return _result;
    }

    private void OnMouseClick(bool value){
        _result = !value;
    }
}
