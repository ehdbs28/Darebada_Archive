using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MiniGameManager : MonoBehaviour, IManager
{
    private List<MiniGameAnswer> _points;

    private int _pointCnt;
    public int PointCnt => _pointCnt;

    public event Action OnAnswerEvent = null;
    public event Action OnWrongEvent = null;

    public void InitManager()
    {
        _points = new List<MiniGameAnswer>();
    }

    public void SetUp(Transform parent){
        // _pointCnt = Random.Range(3, 6);
        _pointCnt = 4;
        
        for(int i = 0; i < _pointCnt; i++){
            float weight = 360f / _pointCnt;

            float point = Random.Range(weight * _points.Count, (weight * (_points.Count + 1)));
            float thickness = Random.Range(weight / _pointCnt, weight - weight / _pointCnt);

            MiniGameAnswer answer = GameManager.Instance.GetManager<PoolManager>().Pop("MiniGameAnswer") as MiniGameAnswer;
            answer.SetUp(point, thickness);

            answer.transform.SetParent(parent);
            answer.SetPosition(Vector3.zero);
            answer.SetScale(Vector3.one);

            _points.Add(answer);
        }
    }

    public void Clear(Transform parent){
        for(int i = 0; i < parent.childCount; i++){
            GameManager.Instance.GetManager<PoolManager>().Push(parent.GetChild(i).GetComponent<MiniGameAnswer>());
        }
    }

    public void Check(){
        bool result = false;

        float cursor = (GameManager.Instance.GetManager<UIManager>().GetPanel(UGUIType.FishingMiniGame) as FishingMiniGamePopup).Angle;
        Debug.Log(cursor);

        for(int i = 0; i < _points.Count; i++){
            result = IsCollisionEnter(_points[i].Point, _points[i].Thickness, cursor);
            if(result)
                break;
        }

        if(result){
            OnAnswerEvent?.Invoke();
        }
    }

    private bool IsCollisionEnter(float center, float thickness, float cursor){
        float min = center;
        float max = center + thickness;

        if(max > 360){
            max -= 360;
        }

        if(min > max){
            return cursor <= max || cursor >= min;
        }

        return min <= cursor && max >= cursor;
    }

    public void ResetManager(){}
    public void UpdateManager(){}
}
