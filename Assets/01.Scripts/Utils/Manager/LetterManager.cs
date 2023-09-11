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
        var aquaManager = GameManager.Instance.GetManager<AquariumNumericalManager>();
        
        int entranceRevenue = aquaManager.CustomerCnt * aquaManager.EntranceFee;
        int etcRevenue = aquaManager.shopRevenue;
        int managerSalary = aquaManager.shopRevenue / 10 * 2;
        int employeeSalary = aquaManager.employeeCnt * 100;
        int manageCost = aquaManager.cleanServiceAmount * 500;

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
        int total = entranceRevenue + etcRevenue + managerSalary + employeeSalary + manageCost;
        
        string title = $"{date.Month}월 {date.Day}일 명세서";
        
        string desc = $"입장료 수익:\n" +
                      $"부가구역 수:\n\n" +
                      $"관리인 임금:\n" +
                      $"직원 임금:\n" +
                      $"아쿠아리움 관리비:\n\n" +
                      $"총 수익:";
        
        string value = $"+{entranceRevenue}\n" +
                       $"+{etcRevenue}\n\n" +
                       $"-{managerSalary}\n" +
                       $"-{employeeSalary}\n" +
                       $"-{manageCost}\n\n" +
                       $"{(total >= 0 ? '+' : '-')}{total}";
        
        string from = "아쿠아리움 관리인";
        LetterUnit letter = new LetterUnit();
        letter.Setup(LetterType.Report, title, desc, date, from, value);
        AddLetter(letter);
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
