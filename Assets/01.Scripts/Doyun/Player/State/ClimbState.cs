using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using PlayerDefine;

public class ClimbState : CommonState
{
    [SerializeField] private float _climbSpeed = 0.5f;
    [SerializeField] private float _moveSpeed = 1.5f;

    [SerializeField]private bool _isReverse = false;

    public override void OnEnterState()
    {
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

        // _isReverse = Vector3.Dot(start, end) < 0;

        Vector3 lookRotation = (new Vector3(end.x, start.y, end.z) - start).normalized;
        lookRotation.y = _parent.position.y;
        _parent.LookAt(_parent.position + lookRotation);

        
        if(!_isReverse){
            yield return StartCoroutine(ClimbUp(start, end, lookRotation, _isReverse));
            yield return StartCoroutine(MoveTo(start, end, _isReverse));
        }
        else{
            yield return StartCoroutine(MoveTo(start, end, _isReverse));
            yield return StartCoroutine(ClimbUp(start, end, lookRotation, _isReverse));
        }
        
        _agentMovement.NavMeshAgent.CompleteOffMeshLink();
        _agentMovement.NavMeshAgent.isStopped = false;

        _agentMovement.NavMeshAgent.updateRotation = true;

        _agentController.ChangeState(StateType.Normal);
    }

    private IEnumerator ClimbUp(Vector3 start, Vector3 end, Vector3 lookRotation, bool isReverse){
        _agentAnimator.SetClimbState(true);
        _parent.LookAt(_parent.position + lookRotation * (isReverse ? -1 : 1));

        float climbTime = Mathf.Abs(end.y - start.y) / _moveSpeed;

        float currentTime = 0f;
        float percent = 0f;

        while(percent < 1){
            currentTime += Time.deltaTime;
            percent = currentTime / climbTime;
            if(!isReverse)
                _parent.position = Vector3.Lerp(start, new Vector3(start.x, end.y, start.z), percent);
            else
                _parent.position = Vector3.Lerp(new Vector3(end.x, start.y, end.z), end, percent);

            yield return null;
        }

        _agentAnimator.SetClimbState(false);
    }

    private IEnumerator MoveTo(Vector3 start, Vector3 end, bool isReverse){
        _agentAnimator.SetWalkState(true);

        float moveTime = Mathf.Abs(end.y - start.y) / _climbSpeed;

        float currentTime = 0f;
        float percent = 0f;

        while(percent < 1){
            currentTime += Time.deltaTime;
            percent = currentTime / (moveTime / 2);
            if(!isReverse)
                _parent.position = Vector3.Lerp(new Vector3(start.x, end.y, start.z), end, percent);
            else
                _parent.position = Vector3.Lerp(start, new Vector3(end.x, start.y, end.z), percent);

            yield return null;
        }
        
        _agentAnimator.SetWalkState(false);
    }
}
