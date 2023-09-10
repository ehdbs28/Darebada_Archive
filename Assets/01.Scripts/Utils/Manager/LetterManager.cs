using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class LetterManager : MonoBehaviour, IManager
{
    public List<LetterUnit> Letters => (GameManager.Instance.GetManager<DataManager>().GetData(DataType.GameData) as GameData)?.HoldingLetter.List;
    
    [SerializeField]
    private LetterTemplateSO ThanksTemplate;
    
    [SerializeField]
    private LetterTemplateSO RequestTemplate;

    [SerializeField]
    [Range(20f, 50f)] 
    private float _sendDelayOffset;

    private float _sendDelay;
    private float _lastSendTime;

    public void InitManager()
    {
        ResetManager();
        
        _lastSendTime = GameManager.Instance.GetManager<TimeManager>().CurrentTime;
        _sendDelay = Random.Range(200, 300f) + Random.Range(0, _sendDelayOffset);

        GameManager.Instance.GetManager<TimeManager>().OnTimeChangedEvent += SendSpecialLetter;
        GameManager.Instance.GetManager<TimeManager>().OnDayChangedEvent += SendReportLetter;
    }

    public void UpdateManager()
    {
        
    }

    private void SendSpecialLetter(int hour, int minute, float currentTime)
    {
        if (currentTime >= _lastSendTime + _sendDelay)
        {
            LetterType type = (LetterType)Random.Range(1, 3);
            GameDate dateTime = GameManager.Instance.GetManager<TimeManager>().DateTime;

            switch (type)
            {
                case LetterType.Thanks:
                    AddThanksLetter(dateTime);
                    break;
                case LetterType.Request:
                    AddRequestLetter(dateTime);
                    break;
            }

            _lastSendTime = currentTime;
            _sendDelay = Random.Range(200, 300f) + Random.Range(0, _sendDelayOffset);
            
            GameManager.Instance.GetManager<UIManager>().Notification("새로운 편지가 도착했습니다.", 1.5f);
        }
    }
    
    private void SendReportLetter(GameDate gameDate)
    {
        int entranceRevenue = 0;
        int etcRevenue = 0;
        int managerSalary = 0;
        int employeeSalary = 0;
        int manageCost = 0;
        AddReportLetter(entranceRevenue, etcRevenue, managerSalary, employeeSalary, manageCost, gameDate);
        
        GameManager.Instance.GetManager<UIManager>().Notification("새로운 편지가 도착했습니다.", 1.5f);
    }
    
    private void AddRequestLetter(GameDate date)
    {
        int requestId = Random.Range(0, RequestTemplate.titles.Count);

        string title = RequestTemplate.titles[requestId];
        string desc = RequestTemplate.descs[requestId];
        string from = RequestTemplate.froms[requestId];
        
        LetterUnit letter = new LetterUnit();
        letter.Setup(LetterType.Request, title, desc, date, from);
        
        AddLetter(letter);
    }
    
    public void AddThanksLetter(GameDate date)
    {
        int id = Random.Range(0, ThanksTemplate.titles.Count);
        
        string title = ThanksTemplate.titles[id];
        string desc = ThanksTemplate.descs[id];
        string from = ThanksTemplate.froms[id];
        
        LetterUnit letter = new LetterUnit();
        letter.Setup(LetterType.Thanks, title, desc, date, from);
        
        AddLetter(letter);
    }
    
    public void AddReportLetter(int entranceRevenue, int etcRevenue, int managerSalary, int employeeSalary, int manageCost, GameDate date)
    {
        // 아쿠아리움 만들고 나서 다시 해야함
        // GameDate n_date = new GameDate(date.Year,date.Month,date.Day);
        // string s_date = $"{date.Month}M {date.Day}D";
        // string title = date + "";//InsertTitle
        // string desc = $"{entranceRevenue}\n{etcRevenue}\n\n{managerSalary}\n{employeeSalary}\n{manageCost}";//InsertDesc
        // string from = "AquariumManager";
        // LetterUnit letter = new LetterUnit();
        // letter.Setup(LetterType.Thanks, title, desc, n_date, from);
        // AddLetter(letter);
    }

    public void RemoveLetter(LetterUnit unit)
    {
        Letters.Remove(unit);
    }

    private void AddLetter(LetterUnit unit){
        Letters.Add(unit);
    }

    public void ResetManager()
    {
        Letters.Clear();
    }
}
