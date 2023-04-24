using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FishingState : MonoBehaviour, IState
{
    protected List<FishingTransition> _transitions;
    protected FishingController _controller;
    protected FishingActionData _actionData;

    public abstract void EnterState();
    public abstract void ExitState();

    public virtual void SetUp(Transform agentRoot)
    {
        _controller = agentRoot.GetComponent<FishingController>();
        _actionData = agentRoot.GetComponent<FishingActionData>();

        _transitions = new List<FishingTransition>();
        transform.GetComponentsInChildren<FishingTransition>(_transitions);

        _transitions.ForEach(t => t.SetUp(agentRoot));
    }

    public virtual void UpdateState()
    {
        foreach(var t in _transitions){
            if(t.CheckDecision()){
                _controller.ChangedState(t.NextState);
                break;
            }
        }
    }
}
