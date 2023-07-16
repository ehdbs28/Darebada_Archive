using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingReelUpState : FishingState
{
    [SerializeField]
    private Transform _endPos;
    private Vector3 _startPos;

    private Transform _bobberTrm;

    private float _reelUpOffset;
    private Vector3 _direction;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);
        _bobberTrm = agentRoot.Find("Bobber");
    }

    public override void EnterState()
    {
        GameManager.Instance.GetManager<UIManager>().ShowPanel(UGUIType.FishingMiniGame, true, false, false);

        _startPos = _bobberTrm.position;

        _direction = (_endPos.position - _startPos).normalized;
        _reelUpOffset = Vector3.Distance(_startPos, _endPos.position) / GameManager.Instance.GetManager<MiniGameManager>().PointCnt;

        GameManager.Instance.GetManager<MiniGameManager>().OnAnswerEvent += OnAnswer;
    }

    public override void ExitState()
    {
        GameManager.Instance.GetManager<MiniGameManager>().OnAnswerEvent -= OnAnswer;
    }

    public override void UpdateState()
    {
        // 이거 조건 나중에 고치기
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log(1);
            GameManager.Instance.GetManager<MiniGameManager>().Check();
        }

        base.UpdateState();
    }

    private void OnAnswer(){
        Debug.Log("미니게임 성공");
        _bobberTrm.position += _direction * _reelUpOffset;
    }
}
