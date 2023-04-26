using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFishingEndDecision : FishingDecision
{
    public override bool MakeADecision()
    {
        return !_controller.ActionData.IsFishing;
    }
}
