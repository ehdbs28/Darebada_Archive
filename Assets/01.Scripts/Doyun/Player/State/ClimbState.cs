using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using PlayerDefine;

public class ClimbState : CommonState
{
    [SerializeField] private float _climbSpeed = 1.5f;

    public override void OnEnterState()
    {
        _agentAnimator.SetClimbState(true);
        StartCoroutine(ClimbOnDescend());
    }

    public override void OnExitState()
    {
        _agentAnimator.SetClimbState(false);
    }

    public override void UpdateState()
    {

    }

    private IEnumerator ClimbOnDescend(){
        _agentMovement.NavMeshAgent.isStopped = true;
        OffMeshLinkData linkData = _agentMovement.NavMeshAgent.currentOffMeshLinkData;
        Vector3 start = linkData.startPos;
        Vector3 end = linkData.endPos;

        float climbTime = Mathf.Abs(end.y - start.y) / _climbSpeed;

        float currentTime = 0f;
        float percent = 0f;

        while(percent < 1){
            currentTime += Time.deltaTime;
            percent = currentTime / climbTime;
            transform.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        _agentMovement.NavMeshAgent.CompleteOffMeshLink();
        _agentMovement.NavMeshAgent.isStopped = false;

        _agentController.ChangeState(StateType.Normal);
    }
}
