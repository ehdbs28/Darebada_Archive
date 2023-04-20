using System.Collections;
using System.Collections.Generic;
using PlayerDefine;
using UnityEngine;
using UnityEngine.AI;

public class DropState : CommonState
{
    [SerializeField] private float _jumpSpeed = 7.0f;
    [SerializeField] private float _gravity = 9.8f;

    [SerializeField] private float _dropSpeed = 7.0f;

    public override void OnEnterState()
    {
        StartCoroutine(Drop());

        _agentAnimator.SetJumpTrigger(true);
        _agentAnimator.SetJumpState(true);
    }

    public override void OnExitState()
    {
        _agentAnimator.SetJumpTrigger(false);
        _agentAnimator.SetJumpState(false);
        _agentAnimator.SetGroundState(true);
    }

    public override void UpdateState()
    {

    }

    private IEnumerator Drop(){
        _agentMovement.NavMeshAgent.isStopped = true;

        OffMeshLinkData linkData = _agentMovement.NavMeshAgent.currentOffMeshLinkData;

        Vector3 start = _parent.position;
        Vector3 end = linkData.endPos;

        Vector3 lookRotation = (new Vector3(end.x, start.y, end.z) - start).normalized;
        lookRotation.y = start.y;
        _parent.LookAt(_parent.position + lookRotation);

        yield return StartCoroutine(JumpTo(start, new Vector3(end.x, start.y, end.z)));

        _agentAnimator.SetJumpState(false);
        _agentAnimator.SetJumpTrigger(false);

        _agentAnimator.SetGroundState(false);
    
        start = new Vector3(end.x, start.y, end.z);

        float dropTime = Mathf.Abs(end.y - start.y) / _dropSpeed;

        float currentTime = 0f;
        float percent = 0f;

        while(percent < 1){
            currentTime += Time.deltaTime;
            percent = currentTime / dropTime;
            _parent.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        _agentAnimator.SetGroundState(true);

        _agentMovement.NavMeshAgent.CompleteOffMeshLink();
        _agentMovement.NavMeshAgent.isStopped = false;

        _agentController.ChangeState(StateType.Normal);
    }

    private IEnumerator JumpTo(Vector3 start, Vector3 end){
        float jumpTime = Mathf.Max(0.3f, Vector3.Distance(start, end)) / _jumpSpeed;

        float currentTime = 0f;
        float percent = 0f;

        float v0 = (end - start).y - _gravity;

        while(percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / jumpTime; 

            Vector3 pos = Vector3.Lerp(start, end, percent);
            pos.y = start.y + (v0 * percent) + (_gravity * percent * percent);
            _parent.position = pos;

            yield return null;
        }
    }
}
