using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommonState : MonoBehaviour, IPlayerState
{
    public abstract void OnEnterState();
    public abstract void OnExitState();
    public abstract void UpdateState();

    protected AgentController _agentController;
    protected AgentCondition _agentCondition;
    protected AgentMovement _agentMovement;
    protected AgentAnimator _agentAnimator;

    protected bool _canUpdateState;
    protected Transform _parent;

    public virtual void SetUp(Transform agentRoot) {
        _canUpdateState = true;
        _parent = agentRoot;

        _agentController = agentRoot.GetComponent<AgentController>();
        _agentCondition = agentRoot.GetComponent<AgentCondition>();
        _agentMovement = agentRoot.GetComponent<AgentMovement>();
        _agentAnimator = agentRoot.Find("Player Mesh").GetComponent<AgentAnimator>();
    }
}
