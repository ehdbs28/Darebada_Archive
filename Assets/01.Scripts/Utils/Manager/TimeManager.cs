using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using static Core.GameTime;
using System;

public class TimeManager : IManager
{
    public float CurrentTime
    {
        get => _gameData.GameTime;
        private set => _gameData.GameTime = value;
    }

    private int TotalDay
    {
        get => _gameData.TotalDay;
        set => _gameData.TotalDay = value;
    }

    public GameDate DateTime
    {
        get => _gameData.GameDateTime;
        private set => _gameData.GameDateTime = value;
    }

    private float _timeScale = 1f;
    public float TimeScale
    {
        get => _timeScale;
        set{
            _timeScale = Mathf.Clamp(value, 0f, 1f);
            Time.timeScale = _timeScale;
        }
    }

    public float Hour => CurrentTime % DayDelay / HourDelay;
    private float Minute => (int)(CurrentTime % DayDelay / MinuteDelay % 12) * 5;


    private GameData _gameData;

    public event Action<int, int, float> OnTimeChangedEvent = null;
    public event Action<GameDate> OnDayChangedEvent = null; 

    public void InitManager()
    {
        _gameData = GameManager.Instance.GetManager<DataManager>().GetData(DataType.GameData) as GameData;
    }

    public void UpdateManager()
    {
        CurrentTime += Time.deltaTime * _timeScale;
        _gameData.GameTime = CurrentTime;
        OnTimeChangedEvent?.Invoke(Mathf.FloorToInt(Hour), Mathf.FloorToInt(Minute), CurrentTime);
        CheckDayCount();
    }

    private void CheckDayCount(){
        if(CurrentTime >= TotalDay * DayDelay){
            ++DateTime.Day;
            ++TotalDay;

            if(DateTime.Day > GameTime.DayPerMonth[DateTime.Month % 12]){
                DateTime.Day = 1;
                ++DateTime.Month;

                if(DateTime.Month > 12){
                    ++DateTime.Year;
                    DateTime.Month = 1; 
                }
            }

            _gameData.TotalDay = TotalDay;
            _gameData.GameDateTime = DateTime;

            OnDayChangedEvent?.Invoke(DateTime);
        }
    }

    public void ResetManager() {}
}
