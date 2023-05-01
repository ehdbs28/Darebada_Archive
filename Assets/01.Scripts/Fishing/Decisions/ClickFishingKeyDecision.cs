using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFishingKeyDecision : FishingDecision
{
    bool _result = false;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);

        InputManager.Instance.OnMouseClickEvent += OnMouseClick;
    }

    public override bool MakeADecision()
    {
        return _result;
    }

    private void OnMouseClick(bool value){
        _result = value;
    }
}
