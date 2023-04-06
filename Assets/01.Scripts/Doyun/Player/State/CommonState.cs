using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommonState : MonoBehaviour, IPlayerState
{
    public abstract void OnEnterState();
    public abstract void OnExitState();
    public abstract void UpdateState();

    protected AgentController _agentController;
    protected AgentInput _agentInput;
    protected AgentMovement _agentMovement;
    protected AgentAnimator _agentAnimator;

    public virtual void SetUp(Transform agentRoot) {
        _agentController = agentRoot.GetComponent<AgentController>();
        _agentInput = agentRoot.GetComponent<AgentInput>();
        _agentMovement = agentRoot.GetComponent<AgentMovement>();
        _agentAnimator = agentRoot.Find("Player Mesh").GetComponent<AgentAnimator>();
    }
}
