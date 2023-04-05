using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    public void OnEnterState();
    public void OnExitState();
    public void UpdateState();
    public void SetUp(Transform agentRoot);
}
