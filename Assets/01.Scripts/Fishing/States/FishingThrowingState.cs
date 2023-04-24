using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingThrowingState : FishingState
{
    private Transform _bobberTrm;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);

        _bobberTrm = agentRoot.Find("Bobber");
    }

    public override void EnterState()
    {
        StartCoroutine(ThrowTo());
    }

    public override void ExitState()
    {
    }

    private IEnumerator ThrowTo(){
        Vector3 start = _bobberTrm.position;
        Vector3 end = _actionData.LastThrowDirection.normalized * _actionData.LastChargingPower;

        Debug.Log(start);
        Debug.Log(end);

        float throwTime = Mathf.Max(0.3f, Vector3.Distance(start, end)) / _actionData.ThrowingSpeed;

        float currentTime = 0f;
        float percent = 0f;

        while(percent < 1){
            currentTime += Time.deltaTime;
            percent = currentTime / throwTime;

            Vector3 pos = Vector3.Slerp(start, end, percent);
            _bobberTrm.position = pos;

            yield return null;
        }
    }
}
