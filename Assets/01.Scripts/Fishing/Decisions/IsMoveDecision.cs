using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMoveDecision : FishingDecision
{
    private BoatController _boatController;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);

        _boatController = GameObject.Find("Boat").GetComponent<BoatController>();
    }

    public override bool MakeADecision()
    {
        return _boatController.BoatData.IsMoveBoat;
    }
}
