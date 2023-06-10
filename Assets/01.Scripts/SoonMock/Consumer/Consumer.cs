using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Consumer : MonoBehaviour
{
    enum STATE
    {
        WALK,
        ACTION
    }
    STATE state;
    bool isWaiting = false;
    public float waittingTime;
    public List<Transform> targets;
    public void Init(Vector3 pos,int cnt, List<Facility> facilities)
    {
        transform.position = pos;
        targets = new List<Transform>();
        for(int i = 0;  i < cnt; i++)
        {
            targets.Add(facilities[Random.Range(0,facilities.Count)].transform);
        }
        //targets.Add(AquariumManager.Instance.endTarget);
    }
    NavMeshAgent _agent;
    private void Update()
    {
        if(state == STATE.WALK)
        {
            _agent.destination = targets[0].position;
            if(Vector3.Distance(transform.position, targets[0].position) < 1)
            {
                state = STATE.ACTION;
            }

        }
        else if(state == STATE.ACTION)
        {
            if(!isWaiting)
            {
                StartCoroutine(Wait());
            }
        }
    }
    IEnumerator Wait()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waittingTime);
        isWaiting = false;
        state = STATE.WALK;
    }
}
