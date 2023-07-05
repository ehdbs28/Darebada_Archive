using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingController : ModuleController
{
    [SerializeField]
    private FishingState _currentState;
    public FishingState CurrentState => _currentState;

    [SerializeField]
    private FishingActionData _actionData;
    public FishingActionData ActionData => _actionData;
    public FishingData FishingData => GameManager.Instance.GetManager<DataManager>().GetData(DataType.FishingData) as FishingData;

    private FishingAnimationController _animatorController;
    public FishingAnimationController AnimatorController => _animatorController;

    private LineRenderer _lineRenderer;
    public LineRenderer LineRenderer => _lineRenderer;

    [SerializeField]
    private Transform _lineStartPos;
    
    [SerializeField]
    private Transform _lineEndPos;

    protected override void Awake() {
        transform.Find("StateManager").GetComponentsInChildren<Module>(_modules);

        _actionData = transform.GetComponent<FishingActionData>();
        _animatorController = transform.Find("Visual").GetComponent<FishingAnimationController>();
        _lineRenderer = transform.GetComponent<LineRenderer>();

        _modules.ForEach(s => s.SetUp(transform));
    }

    protected override void Start() {
        // ì²˜ìŒ ?œì‘????ê¸°ë³¸ ?íƒœë¡??ê¸° ?„í•¨
        ChangedState(_currentState);
    }

    protected override void Update() {
        _currentState?.UpdateModule();

        _lineRenderer.SetPosition(0, _lineStartPos.position);
        _lineRenderer.SetPosition(1, _lineEndPos.position);
    }

    public void ChangedState(FishingState nextState){
        _currentState?.ExitState();
        _currentState = nextState;
        _currentState?.EnterState();
    }
}
