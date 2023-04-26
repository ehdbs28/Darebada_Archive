using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFishingKeyDecision : FishingDecision
{
    public override bool MakeADecision()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
