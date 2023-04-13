using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using PlayerDefine;

public class JumpState : CommonState
{
    [SerializeField] private float _jumpSpeed = 7.0f;
    [SerializeField] private float _gravity = -9.8f;

    public override void OnEnterState()
    {
        StartCoroutine(JumpTo());
        _agentAnimator.SetJumpTrigger(true);
        _agentAnimator.SetJumpState(true);
    }

    public override void OnExitState()
    {
        _agentAnimator.SetJumpState(false);
        _agentAnimator.SetJumpTrigger(false);
    }

    public override void UpdateState()
    {

    }

    private IEnumerator JumpTo(){
        _agentMovement.NavMeshAgent.isStopped = true;

        OffMeshLinkData linkData = _agentMovement.NavMeshAgent.currentOffMeshLinkData;

        Vector3 start = _parent.position;
        Vector3 end = linkData.endPos;

        float jumpTime = Mathf.Max(0.3f, Vector3.Distance(start, end)) / _jumpSpeed;

        float currentTime = 0f;
        float percent = 0f;

        float v0 = (end - start).y - _gravity;

        Vector3 lookRotation = (new Vector3(end.x, start.y, end.z) - start).normalized;
        lookRotation.y = start.y;
        _parent.LookAt(_parent.position + lookRotation);

        while(percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / jumpTime; 

            Vector3 pos = Vector3.Lerp(start, end, percent);
            pos.y = start.y + (v0 * percent) + (_gravity * percent * percent);
            _parent.position = pos;

            yield return null;
        }

        _agentMovement.NavMeshAgent.CompleteOffMeshLink();
        _agentMovement.NavMeshAgent.isStopped = false;

        _agentController.ChangeState(StateType.Normal);
    }
}
