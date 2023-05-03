using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

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

    public void InitManager()
    {
        GameData gameData = GameManager.Instance.GetManager<DataManager>().GetData(DataType.GameData) as GameData;
        
        _currentTime = gameData.LastWorldTime;
        _currentDay = gameData.LastDay;
    }

    public void UpdateManager()
    {
        _currentTime += Time.deltaTime * _timeScale;

        if(_currentTime >= ((_currentDay + 1) * Define.DayCycle)){
            // 하루가 지남
            ++_currentDay;
            Debug.Log($"DDAY - {_currentDay}");
        }
    }

    public void Reset() {}
}
