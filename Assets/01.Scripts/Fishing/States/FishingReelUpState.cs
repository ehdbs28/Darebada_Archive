using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGamePoint{
    public float Point;
    public float Thickness;

    public MiniGamePoint(float point, float thickness){
        Point = point;
        Thickness = thickness;
    }
}

public class MiniGameCircle
{
    private List<MiniGamePoint> _points;
    private int _pointCnt;

    public MiniGameCircle(int pointCount){
        _pointCnt = pointCount;
    }

    private void SetUp(){
        for(int i = 0; i < _pointCnt; i++){
            float weight = 360f / _pointCnt;

            float point = Random.Range(weight * _points.Count, (weight * (_points.Count) + 1));
            float thickness = Random.Range(weight / _pointCnt, weight - weight / _pointCnt);

            _points.Add(new MiniGamePoint(point, thickness));
        }
    }

    public bool Check(float cursor){
        bool result = false;

        for(int i = 0; i < _points.Count; i++){
            result = IsCollisionEnter(_points[i].Point, _points[i].Thickness, cursor);
            if(result)
                return result;
        }

        return result;
    }

    private bool IsCollisionEnter(float center, float thickness, float cursor){
        return (center - thickness >= cursor) && (center + thickness <= cursor);
    }
}

public class FishingReelUpState : FishingState
{
    [SerializeField]
    private Transform _endPos;
    private Vector3 _startPos;

    private Transform _bobberTrm;

    private int _targetCnt;
    private float _reelUpOffset;
    private Vector3 _direction;

    private MiniGameCircle _circle;
    public MiniGameCircle Circle => _circle;

    private float _cursor;
    public float Cursor => _cursor;

    [SerializeField]
    private float _cursorSpeed;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);
        _bobberTrm = agentRoot.Find("Bobber");
    }

    public override void EnterState()
    {
        _startPos = _bobberTrm.position;

        _targetCnt = Random.Range(3, 6);
        _direction = (_endPos.position - _startPos).normalized;
        _reelUpOffset = Vector3.Distance(_startPos, _endPos.position) / _targetCnt;

        _circle = new MiniGameCircle(_targetCnt);
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        _cursor += _cursorSpeed * Time.deltaTime;
        if(_cursor >= 360f){
            _cursor = 0f;
        }

        // 이거 조건 나중에 고치기
        if(Input.GetKeyDown(KeyCode.Space)){
            CheckAnswer();
        }

        base.UpdateState();
    }

    private void CheckAnswer(){
        if(_circle.Check(_cursor)){
            Debug.Log("미니게임 성공");
        }
        else{
            Debug.Log("미니게임 실패");
        }
    }
}
