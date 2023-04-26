using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsUnderWaterDecision : FishingDecision
{
    public override bool MakeADecision()
    {
        return _controller.ActionData.IsUnderWater;
    }
}
