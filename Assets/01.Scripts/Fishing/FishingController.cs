using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingController : MonoBehaviour
{
    [SerializeField]
    private FishingState _currentState;

    private void Awake() {
        List<FishingState> states = new List<FishingState>();
        transform.Find("StateManager").GetComponentsInChildren<FishingState>(states);

        states.ForEach(s => s.SetUp(transform));
    }

    private void Start() {
        // 처음 시작할 때 기본 상태로 두기 위함
        ChangedState(_currentState);
    }

    private void Update() {
        _currentState?.UpdateState();
    }

    public void ChangedState(FishingState nextState){
        _currentState?.ExitState();
        _currentState = nextState;
        _currentState?.EnterState();
    }
}
