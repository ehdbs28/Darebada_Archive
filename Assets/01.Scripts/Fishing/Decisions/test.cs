using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : FishingDecision
{
    public override bool MakeADecision()
    {
        return Input.GetKeyDown(KeyCode.V);
    }
}
