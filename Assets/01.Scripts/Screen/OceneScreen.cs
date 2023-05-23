using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OceneScreen : UIScreen
{
    private Label _timeText;
    private Label _dateText;

    private VisualElement _settingBtn;
    private VisualElement _gobackBtn;
    private VisualElement _letterBtn;
    private VisualElement _dictionaryBtn;
    private VisualElement _inventoryBtn;

    protected override void AddEvent(VisualElement root)
    {
        GameManager.Instance.GetManager<TimeManager>().OnTimeChangedEvent += OnChangedTime;
        GameManager.Instance.GetManager<TimeManager>().OnDayChangedEvent += OnChangedDay;

        _letterBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.Letter);
        });
    }

    protected override void FindElement(VisualElement root)
    {
        _timeText = root.Q<Label>("time-text");
        _dateText = root.Q<Label>("date-text");

        _settingBtn = root.Q<VisualElement>("setting-btn");
        _gobackBtn = root.Q<VisualElement>("goto-btn");
        _letterBtn = root.Q<VisualElement>("letter-btn");
        _dictionaryBtn = root.Q<VisualElement>("dictionary-btn");
        _inventoryBtn = root.Q<VisualElement>("inventory-btn");
    }

    private void OnChangedTime(int hour, int minute){
        _timeText.text = $"{hour.ToString("D2")}:{minute.ToString("D2")}";
    }

    private void OnChangedDay(int year, int month, int day){
        _dateText.text = $"{year}년째, {month}월{day}일";
    }
}
