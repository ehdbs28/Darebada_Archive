using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour, IManager
{
    private List<MiniGameAnswer> _points;

    private Transform _parentTrm = null;

    private int _pointCnt;
    public int PointCnt => _pointCnt;

    public void InitManager()
    {
        _points = new List<MiniGameAnswer>();
    }

    public void SetUp(Transform parent){
        _pointCnt = Random.Range(3, 6);
        _parentTrm = parent;
        
        for(int i = 0; i < _pointCnt; i++){
            float weight = 360f / _pointCnt;

            float point = Random.Range(weight * _points.Count, (weight * (_points.Count + 1)));
            float thickness = Random.Range(weight / _pointCnt, weight - weight / _pointCnt);

            MiniGameAnswer answer = GameManager.Instance.GetManager<PoolManager>().Pop("MiniGameAnswer") as MiniGameAnswer;
            answer.SetUp(point, thickness);

            answer.transform.SetParent(_parentTrm);
            answer.SetPosition(Vector3.zero);
            answer.SetScale(Vector3.one);

            _points.Add(answer);
        }
    }

    public void Resetting()
    {
        if (_parentTrm == null)
            return;

        Transform temp = _parentTrm;
        Clear();
        SetUp(temp);
    }

    public void Clear()
    {
        if (_parentTrm == null)
            return;
        
        for(int i = 0; i < _parentTrm.childCount; i++){
            GameManager.Instance.GetManager<PoolManager>().Push(_parentTrm.GetChild(i).GetComponent<MiniGameAnswer>());
        }
        _parentTrm = null;
    }

    public bool Check(){
        bool result = false;

        float cursor = (GameManager.Instance.GetManager<UIManager>().GetPanel(UGUIType.FishingMiniGame) as FishingMiniGamePopup).Angle;

        for(int i = 0; i < _points.Count; i++){
            result = IsCollisionEnter(_points[i].Point, _points[i].Thickness, cursor);
            print(result);
            if(result)
                break;
        }

        return result;
    }

    private bool IsCollisionEnter(float center, float thickness, float cursor){
        float min = center;
        float max = center + thickness;

        if(max > 360){
            max %= 360;
        }
        if (min > 360){
            min %= 360;
        }

        print($"min : {min}, max : {max}, cursor : {cursor}");
        if(min > max){
            return cursor <= max || cursor >= min;
        }

        return min <= cursor && max >= cursor;
    }

    public void ResetManager(){}
    public void UpdateManager(){}
}
