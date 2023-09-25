using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingCatchingState : FishingState
{
    private Transform _bobberTrm;

    private bool _isReadyToCatch;
    public bool IsReadyToCatch => _isReadyToCatch;

    private Vector3 _start;
    private Vector3 _end;

    private float _stringLength
    {
        get
        {
            var sheetdata = (FishingUpgradeTable)GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.FishingUpgradeData);
            var data = (FishingData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.FishingData);

            return sheetdata.DataTable[1].Value[data.StringLength_Level-1];
        }
    }

    private float _throwTime;
    private float _currentTime = 0f;
    private float _percent = 0f;
    private float percent {
        get {
            return _percent;
        }
        set{
            _percent = value;
            (GameManager.Instance.GetManager<UIManager>().GetPanel(ScreenType.Fishing) as FishingScreen).SetHeight(_percent, _percent * _stringLength);
        }
    }

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);

        _bobberTrm = agentRoot.Find("Bobber");
    }

    public override void EnterState()
    {
        ((FishingScreen)GameManager.Instance.GetManager<UIManager>().GetPanel(ScreenType.Fishing))
            .ReelUpBtnClickEvent += ReelUp;
        
        GameManager.Instance.GetManager<UIManager>().ShowPanel(ScreenType.Fishing);

        _start = _bobberTrm.position;
        _end = _start + _controller.ActionData.LastThrowDirection.normalized; 

        _end.y = -_stringLength;

        _throwTime = Mathf.Max(0.3f, Vector3.Distance(_start, _end)) / _controller.DataSO.ThrowingSpeed;
        _currentTime = 0f;
        percent = 0f;

        _isReadyToCatch = false;

        StartCoroutine(ToThrow());
    }

    public override void ExitState()
    {
        ((FishingScreen)GameManager.Instance.GetManager<UIManager>().GetPanel(ScreenType.Fishing))
            .ReelUpBtnClickEvent -= ReelUp;
        
        GameManager.Instance.GetManager<InputManager>().OnTouchEvent -= OnTouch;
    }

    private void ReelUp()
    {
        percent -= _controller.DataSO.ThrowingSpeed * Time.deltaTime / _throwTime;
        _bobberTrm.position = GetLerpPos();

        if (percent <= 0)
        {
            if (_controller.Bait.CatchedFish != null)
            {
                _controller.Bait.CatchedFish.GetoutBait();    
                _controller.Bait.CatchedFish = null;
            }
            
            _controller.ActionData.IsFishing = false;
            _controller.ActionData.IsUnderWater = false;
        }
    }

    private void OnTouch()
    {
        if(!_isReadyToCatch){
            percent = 1f;
            _isReadyToCatch = true;
            _controller.Bait.StartCheck = true;
        }
    }

    private IEnumerator ToThrow(){
        while(percent < 1 && _isReadyToCatch == false){
            _currentTime += _controller.DataSO.ThrowingSpeed * Time.deltaTime;
            percent = _currentTime / _throwTime;

            _bobberTrm.position = GetLerpPos();

            yield return null;
        }

        percent = 1f;
        _bobberTrm.position = GetLerpPos();

        _isReadyToCatch = true;
        _controller.Bait.StartCheck = true;

        //_start = new Vector3(_controller.ActionData.InitPosition.x, 0, _controller.ActionData.InitPosition.z);
        GameManager.Instance.GetManager<CameraManager>()._isCanRotate = true;
    }

    private Vector3 GetLerpPos(){
        return Vector3.Lerp(_start, _end, percent);
    }
}
