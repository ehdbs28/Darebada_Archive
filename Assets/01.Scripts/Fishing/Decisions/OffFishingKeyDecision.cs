using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffFishingKeyDecision : FishingDecision
{
    public override bool MakeADecision()
    {
        return Input.GetKeyUp(KeyCode.Space);
    }
}
