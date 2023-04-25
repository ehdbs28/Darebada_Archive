using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingThrowingState : FishingState
{
    private Transform _bobberTrm;
    private Transform _playerTrm;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);

        _bobberTrm = agentRoot.Find("Bobber");
        _playerTrm = agentRoot.parent;
    }

    public override void EnterState()
    {
        _playerTrm.LookAt(_actionData.LastThrowDirection);
        StartCoroutine(ThrowTo());
    }

    public override void ExitState()
    {
    }

    private IEnumerator ThrowTo(){
        Vector3 start = _bobberTrm.position;
        Vector3 end = _playerTrm.position + _actionData.LastThrowDirection.normalized * _actionData.LastChargingPower;

        float throwTime = Mathf.Max(0.3f, Vector3.Distance(start, end)) / _actionData.ThrowingSpeed;

        float currentTime = 0f;
        float percent = 0f;

        float v0 = (end - start).y + 9.8f;

        while(percent < 1){
            currentTime += _actionData.ThrowingSpeed * Time.deltaTime;
            percent = currentTime / throwTime;

            Vector3 pos = Vector3.Lerp(start, end, percent);
            pos.y = start.y + (v0 * percent) +  (-9.8f * percent * percent);
            _bobberTrm.position = pos;

            yield return null;
        }
    }
}
