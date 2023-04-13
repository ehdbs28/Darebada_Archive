using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerDefine;
using System;

public class AgentController : MonoBehaviour
{
    private Dictionary<StateType, IPlayerState> _stateDictionary;
    private IPlayerState _currentState;

    private void Awake() {
        _stateDictionary = new Dictionary<StateType, IPlayerState>();

        Transform stateTrm = transform.Find("States");

        foreach(StateType state in Enum.GetValues(typeof(StateType))){
            IPlayerState stateSrc = stateTrm.Find($"{state}State").GetComponent<IPlayerState>();

            if(stateSrc == null){
                Debug.LogError($"{state} 상태가 Player안에 존재하지 않음.");
                return;
            }

            stateSrc.SetUp(transform);
            _stateDictionary.Add(state, stateSrc);
        }
    }

    private void Start() {
        ChangeState(StateType.Normal);
    }

    public void ChangeState(StateType nextState){
        _currentState?.OnExitState();
        _currentState = _stateDictionary[nextState];
        _currentState?.OnEnterState();
    }

    private void Update() {
        _currentState?.UpdateState();
    }
}
