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

    [SerializeField]
    private FishingDataSO _dataSO;
    public FishingDataSO DataSO => _dataSO;

    private FishingAnimationController _animatorController;
    public FishingAnimationController AnimatorController => _animatorController;

    private LineRenderer _lineRenderer;

    [SerializeField]
    private Transform _lineStartPos;
    
    [SerializeField]
    private Transform _lineEndPos;

    private Bait _bait;
    public Bait Bait => _bait;

    private void Awake() {
        List<FishingState> states = new List<FishingState>();
        transform.Find("StateManager").GetComponentsInChildren<FishingState>(states);

        _bait = GameObject.Find("Bobber").GetComponent<Bait>();

        _actionData = transform.GetComponent<FishingActionData>();
        _animatorController = transform.Find("Visual").GetComponent<FishingAnimationController>();
        _lineRenderer = transform.GetComponent<LineRenderer>();

        states.ForEach(s => s.SetUp(transform));
    }

    private void Start() {
        // 처음 시작할 때 기본 상태로 두기 위함
        ChangedState(_currentState);
    }

    private void Update() {
        _currentState?.UpdateState();

        _lineRenderer.SetPosition(0, _lineStartPos.position);
        _lineRenderer.SetPosition(1, _lineEndPos.position);
    }

    public void ChangedState(FishingState nextState){
        Debug.Log(nextState);
        _currentState?.ExitState();
        _currentState = nextState;
        _currentState?.EnterState();
    }
}
