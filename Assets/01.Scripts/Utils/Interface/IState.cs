using UnityEngine;

public interface IState
{
    public void EnterState();
    public void ExitState();
    public void UpdateState();
    public void SetUp(Transform agentRoot);
}
