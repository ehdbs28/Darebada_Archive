using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFishCatchDecision : FishingDecision
{
    public override bool MakeADecision()
    {
        if (_controller.Bait.Sense)
        {
            _controller.Bait.StartCheck = false;
            return true;
        }

        return false;
    }
}
