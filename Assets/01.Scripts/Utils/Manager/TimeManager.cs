using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using static Core.GameTime;

public class TimeManager : IManager
{
    // 하루에 12분
    private float _currentTime = 0f;
    private int _totalDay = 1;

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
    public float Hour =>_currentTime % DayDelay / HourDelay;
    public float Minute => _currentTime % DayDelay / MinuteDelay % 60;
    public float Second => _currentTime % DayDelay / SecondDelay % 60;

    public int Year { get; private set; } = 0;
    public int Month { get; private set; } = 3;
    public int Day { get; private set; } = 1;

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
        CheckDayCount();
    }

    private void CheckDayCount(){
        if(_currentTime >= _totalDay * DayDelay){
            // 하루가 지남
            GameManager.Instance.GetManager<LetterManager>().SendReportLetter();

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
            
            Debug.Log($"{Year}년째 {Month}월 {Day}일");
        }
    }

    public void ResetManager() {}
}
