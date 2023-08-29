using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CampScreen : UIScreen
{
    private Label _timeText;
    private Label _dateText;
    private Label _goldText;

    private VisualElement _settingBtn;
    private VisualElement _letterBtn;
    private VisualElement _dictionaryBtn;

    public override void AddEvent()
    {
        GameManager.Instance.GetManager<TimeManager>().OnTimeChangedEvent += OnChangedTime;
        GameManager.Instance.GetManager<TimeManager>().OnDayChangedEvent += OnChangedDay;

        _settingBtn.RegisterCallback<ClickEvent>(e =>
        {
            GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.Setting);
        });

        _letterBtn.RegisterCallback<ClickEvent>(e =>
        {
            GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.Letter);
        });

        _dictionaryBtn.RegisterCallback<ClickEvent>(e =>
        {
            GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.Dictionary);
        });

        //_goldText.text = $"{MoneyManager.Instance.goldTxt.text}";
    }
    
    public override void RemoveEvent()
    {
        GameManager.Instance.GetManager<TimeManager>().OnTimeChangedEvent -= OnChangedTime;
        GameManager.Instance.GetManager<TimeManager>().OnDayChangedEvent -= OnChangedDay;
    }

    public override void FindElement()
    {
        _timeText = _root.Q<Label>("time-text");
        _dateText = _root.Q<Label>("date-text");
        _goldText = _root.Q("money-container").Q<Label>("text");

        _settingBtn = _root.Q<VisualElement>("setting-btn");
        _letterBtn = _root.Q<VisualElement>("letter-btn");
        _dictionaryBtn = _root.Q<VisualElement>("dictionary-btn");
    }

    private void OnChangedTime(int hour, int minute)
    {
        _timeText.text = $"{hour.ToString("D2")}:{minute.ToString("D2")}";
    }

    private void OnChangedDay(int year, int month, int day)
    {
        _dateText.text = $"{year}년째, {month}월{day}일";
    }
}