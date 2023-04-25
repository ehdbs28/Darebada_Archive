using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingIdleState : FishingState
{
    public override void EnterState()
    {
        _actionData.IsFishing = false;
    }

    public override void ExitState()
    {
    }
}
