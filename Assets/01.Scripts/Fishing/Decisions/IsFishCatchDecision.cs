using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFishCatchDecision : FishingDecision
{
    public override bool MakeADecision()
    {
        return _controller.Bait.Sense;
    }
}
