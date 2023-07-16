using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using static Core.GameTime;
using System;

public class TimeManager : IManager
{
    private float _currentTime = 0f;
    private int _totalDay = 0;

    private float _timeScale = 1f;
    public float TimeScale {
        get{
            return _timeScale;
        }
        set{
            if(value < 0f){
                Time.timeScale = 0f;
                _timeScale = 0f;
                return;
            }

            _timeScale = value;
            Time.timeScale = _timeScale;
        }
    }

    public float Hour => _currentTime % DayDelay / HourDelay;
    public float Minute => (int)(_currentTime % DayDelay / MinuteDelay % 12) * 5;

    public GameDate DateTime { get; private set; } = new GameDate(0, 3, 0);

    public event Action<int, int> OnTimeChangedEvent = null;
    public event Action<int, int, int> OnDayChangedEvent = null; 

    public void InitManager()
    {
        GameData gameData = GameManager.Instance.GetManager<DataManager>().GetData(DataType.GameData) as GameData;
        
        _currentTime = gameData.GameTime;
        _totalDay = gameData.TotalDay;
        OnDayChangedEvent += SendSpecification;
        DateTime = new GameDate(gameData.GameDateTime.Year, gameData.GameDateTime.Month, gameData.GameDateTime.Day);
    }
    public void SendSpecification(int year, int month, int day)
    {
        int ent=0, etc=0, man=0, empl=0,manage=0;
        GameManager.Instance.GetManager<LetterManager>().SendReportLetter(ent, etc, man, empl, manage, DateTime);
    }
    public void SendRequestMail(int year, int month, int day)
    {
        GameManager.Instance.GetManager<LetterManager>().SendRequestLetter(DateTime);
    }
    public void SendReviewMail(int year, int month , int day)
    {
        GameManager.Instance.GetManager<LetterManager>().SendReviewLetter(DateTime);
    }

    public void UpdateManager()
    {
        _currentTime += Time.deltaTime * _timeScale;
        OnTimeChangedEvent?.Invoke(Mathf.FloorToInt(Hour), Mathf.FloorToInt(Minute));
        CheckDayCount();
    }

    private void CheckDayCount(){
        if(_currentTime >= _totalDay * DayDelay){
            // ?�루가 지??
            //GameManager.Instance.GetManager<LetterManager>().SendReportLetter();

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

            OnDayChangedEvent?.Invoke(DateTime.Year, DateTime.Month, DateTime.Day);
        }
    }

    public void ResetManager() {}
}
