using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using static Core.GameTime;
using System;

public class TimeManager : IManager
{
    // ?˜ë£¨??12ë¶?
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

    // ?˜ì¹˜?ì¸ ?œê°„ [ ?¬ìš© ???Œì—??Mathf.floorToInt ?´ì¤˜???•ìƒ?ìœ¼ë¡?ì¶œë ¥ ??]
    public int Hour => (int)(_currentTime % DayDelay / HourDelay);
    public int Minute => (int)(_currentTime % DayDelay / MinuteDelay % 12) * 5;

    public int Year { get; private set; } = 0;
    public int Month { get; private set; } = 3;
    public int Day { get; private set; } = 0;

    // ?´ë²¤??
    public event Action<int, int> OnTimeChangedEvent = null;
    public event Action<int, int, int> OnDayChangedEvent = null; 

    public void InitManager()
    {
        GameData gameData = GameManager.Instance.GetManager<DataManager>().GetData(DataType.GameData) as GameData;
        
        _currentTime = gameData.LastWorldTime;
        _totalDay = gameData.LastTotalDay;

        Year = gameData.LastYear;
        Month = gameData.LastMonth;
        Day = gameData.LastDay;
    }

    public void UpdateManager()
    {
        _currentTime += Time.deltaTime * _timeScale;
        OnTimeChangedEvent?.Invoke(Hour, Minute);
        CheckDayCount();
    }

    private void CheckDayCount(){
        if(_currentTime >= _totalDay * DayDelay){
            // ?˜ë£¨ê°€ ì§€??
            //GameManager.Instance.GetManager<LetterManager>().SendReportLetter();

            ++Day;
            ++_totalDay;

            if(Day > GameTime.DayPerMonth[Month % 12]){
                Day = 1;
                ++Month;

                if(Month > 12){
                    ++Year;
                    Month = 1; 
                }
            }

            OnDayChangedEvent?.Invoke(Year, Month, Day);
        }
    }

    public void ResetManager() {}
}
