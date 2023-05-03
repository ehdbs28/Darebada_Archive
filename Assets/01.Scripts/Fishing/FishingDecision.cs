using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FishingDecision : MonoBehaviour
{
    protected FishingController _controller;

    public bool IsReverse = false;

    public virtual void SetUp(Transform agentRoot){
        _controller = agentRoot.GetComponent<FishingController>();  
    }

    public abstract bool MakeADecision();
}
