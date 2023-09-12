using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using static Core.GameTime;
using System;

public class TimeManager : IManager
{
    private float _currentTime = 0f;
    public float CurrentTime => _currentTime;
    
    private int _totalDay = 0;
    
    private float _timeScale = 1f;
    public float TimeScale
    {
        get => _timeScale;
        set{
            _timeScale = Mathf.Clamp(value, 0f, 1f);
            Time.timeScale = _timeScale;
        }
    }

    public float Hour => _currentTime % DayDelay / HourDelay;
    public float Minute => (int)(_currentTime % DayDelay / MinuteDelay % 12) * 5;

    public GameDate DateTime { get; private set; } = new GameDate(0, 3, 0);

    private GameData _gameData;

    public event Action<int, int, float> OnTimeChangedEvent = null;
    public event Action<GameDate> OnDayChangedEvent = null; 

    public void InitManager()
    {
        _gameData = GameManager.Instance.GetManager<DataManager>().GetData(DataType.GameData) as GameData;
        
        _currentTime = _gameData.GameTime;
        _totalDay = _gameData.TotalDay;
        DateTime = _gameData.GameDateTime;
    }

    public void UpdateManager()
    {
        _currentTime += Time.deltaTime * _timeScale;
        _gameData.GameTime = _currentTime;
        OnTimeChangedEvent?.Invoke(Mathf.FloorToInt(Hour), Mathf.FloorToInt(Minute), _currentTime);
        CheckDayCount();
    }

    private void CheckDayCount(){
        if(_currentTime >= _totalDay * DayDelay){
            ++DateTime.Day;
            ++_totalDay;

            if(DateTime.Day > GameTime.DayPerMonth[DateTime.Month % 12]){
                DateTime.Day = 1;
                ++DateTime.Month;

                if(DateTime.Month > 12){
                    ++DateTime.Year;
                    DateTime.Month = 1; 
                }
            }

            _gameData.TotalDay = _totalDay;
            _gameData.GameDateTime = DateTime;

            OnDayChangedEvent?.Invoke(DateTime);
        }
    }

    public void ResetManager() {}
}
