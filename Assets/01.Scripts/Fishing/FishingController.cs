using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingController : MonoBehaviour
{
    [SerializeField]
    private FishingState _currentState;
    public FishingState CurrentState => _currentState;

    [SerializeField]
    private FishingActionData _actionData;
    public FishingActionData ActionData => _actionData;

    private void Awake() {
        List<FishingState> states = new List<FishingState>();
        transform.Find("StateManager").GetComponentsInChildren<FishingState>(states);

        _actionData = transform.GetComponent<FishingActionData>();

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
        Debug.Log(nextState);

        _currentState?.ExitState();
        _currentState = nextState;
        _currentState?.EnterState();
    }
}
