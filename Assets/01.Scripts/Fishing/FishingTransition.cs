using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingTransition : MonoBehaviour
{
    [SerializeField]
    private FishingState _nextState;
    public FishingState NextState => _nextState;

    private List<FishingDecision> _decisions;

    public void SetUp(Transform agentRoot){
        _decisions = new List<FishingDecision>();
        GetComponents<FishingDecision>(_decisions);

        _decisions.ForEach(d => d.SetUp(agentRoot));
    }

    public bool CheckDecision(){
        bool result = false;

        foreach(var d in _decisions){
            result = d.MakeADecision();

            if(d.IsReverse)
                result = !result;

            if(result == false)
                break;
        }

        return result;
    }
}
