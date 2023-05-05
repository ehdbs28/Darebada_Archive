using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using static Core.Define;

public class TimeManager : IManager
{
    // 하루에 12분
    private float _currentTime = 0f;
    private int _currentDay = 0;

    private float _timeScale = 1f;
    public float TimeScale {
        get{
            return _timeScale;
        }
        set{
            if(value < 0f){
                _timeScale = 0f;
                return;
            }

            _timeScale = value;
        }
    }

    // 수치적인 시간 [ 사용 할 때에는 Mathf.floorToInt 해줘야 정상적으로 출력 됨 ]
    public float Hour =>_currentTime / HourDelay;
    public float Minute => _currentTime / MinuteDelay % 60;
    public float Second => _currentTime / SecondDelay % 60;

    public void InitManager()
    {
        GameData gameData = GameManager.Instance.GetManager<DataManager>().GetData(DataType.GameData) as GameData;
        
        _currentTime = gameData.LastWorldTime;
        _currentDay = gameData.LastDay;
    }

    public void UpdateManager()
    {
        _currentTime += Time.deltaTime * _timeScale;

        if(_currentTime >= DayDelay){
            // 하루가 지남
            ++_currentDay;
            _currentTime = 0f;
            Debug.Log($"DDAY - {_currentDay}");
        }
    }

    public void Reset() {}
}
