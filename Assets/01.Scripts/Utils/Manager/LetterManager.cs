using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class LetterManager : IManager
{
    private List<LetterUnit> _letters;

    public LetterManager(){
        ResetManager();
    }

    public void InitManager()
    {

    }

    public void UpdateManager()
    {
    }

    public void SendReportLetter(){
        string title = $"{GameManager.Instance.GetManager<TimeManager>().Year}년째 {GameManager.Instance.GetManager<TimeManager>().Month}월 {GameManager.Instance.GetManager<TimeManager>().Day}일 아쿠아리움 명세서";
        string desc = $"대충 명세서 내용";
        string date = $"{GameManager.Instance.GetManager<TimeManager>().Year}년째 {GameManager.Instance.GetManager<TimeManager>().Month}월 {GameManager.Instance.GetManager<TimeManager>().Day}일";
        string from = $"아쿠아리움 관리인";

        LetterUnit report = new LetterUnit(LetterType.REPORT, title, desc, date, from);

        AddLetter(report);
    }

    private void AddLetter(LetterUnit data){
        _letters.Add(data);
    }

    public void ResetManager()
    {
        _letters = new List<LetterUnit>();
    }
}
