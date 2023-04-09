using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using PlayerDefine;

public class ClimbState : CommonState
{
    [SerializeField] private float _climbSpeed = 0.5f;

    public override void OnEnterState()
    {
        _agentAnimator.SetClimbState(true);
        StartCoroutine(ClimbOnDescend());
    }

    public override void OnExitState()
    {
        _agentAnimator.SetClimbState(false);
        _agentAnimator.SetWalkState(false);
    }

    public override void UpdateState()
    {

    }

    private IEnumerator ClimbOnDescend(){
        _agentMovement.NavMeshAgent.updateRotation = false;

        _agentMovement.NavMeshAgent.isStopped = true;
        OffMeshLinkData linkData = _agentMovement.NavMeshAgent.currentOffMeshLinkData;
        Vector3 start = linkData.startPos;
        Vector3 end = linkData.endPos;

        Vector3 lookRotation = _parent.position + ((new Vector3(end.x, start.y, end.z) - start).normalized);
        lookRotation.y = _parent.position.y;
        _parent.LookAt(lookRotation);

        float climbTime = Mathf.Abs(end.y - start.y) / _climbSpeed;

        float currentTime = 0f;
        float percent = 0f;

        while(percent < 1){
            currentTime += Time.deltaTime;
            percent = currentTime / climbTime;
            _parent.position = Vector3.Lerp(start, new Vector3(start.x, end.y, start.z), percent);

            yield return null;
        }

        _agentAnimator.SetClimbState(false);
        _agentAnimator.SetWalkState(true);

        percent = 0f;
        currentTime = 0f;

        while(percent < 1){
            currentTime += Time.deltaTime;
            percent = currentTime / (climbTime / 2);
            _parent.position = Vector3.Lerp(new Vector3(start.x, end.y, start.z), end, percent);

            yield return null;
        }

        _agentMovement.NavMeshAgent.CompleteOffMeshLink();
        _agentMovement.NavMeshAgent.isStopped = false;

        _agentMovement.NavMeshAgent.updateRotation = true;

        _agentController.ChangeState(StateType.Normal);
    }
}
