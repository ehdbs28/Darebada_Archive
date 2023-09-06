using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class FishingState : CommonModule<FishingController>
{
    protected List<FishingTransition> _transitions;

    public abstract void EnterState();
    public abstract void ExitState();

    public override void SetUp(Transform rootTrm)
    {
        base.SetUp(rootTrm);

        _transitions = new List<FishingTransition>();
        transform.GetComponentsInChildren<FishingTransition>(_transitions);

        _transitions.ForEach(t => t.SetUp(rootTrm));
    }

    public override void UpdateModule()
    {
        foreach(var t in _transitions){
            if(t.CheckDecision()){
                _controller.ChangedState(t.NextState);
                break;
            }
        }
    }

    public override void FixedUpdateModule(){}
}
