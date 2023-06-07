using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AquariumEditScreen : UIScreen
{
    private Label _timeText;
    private Label _dateText;
    private Label _goldText;

    private VisualElement _settingBtn;
    private VisualElement _backBtn;

    private VisualElement _addTankBtn;
    private VisualElement _addPlantBtn;

    protected override void AddEvent(VisualElement root)
    {
        GameManager.Instance.GetManager<TimeManager>().OnTimeChangedEvent += OnChangedTime;
        GameManager.Instance.GetManager<TimeManager>().OnDayChangedEvent += OnChangedDay;

        _settingBtn.RegisterCallback<ClickEvent>(e => {
            //GameManager.Instance.GetManager<UIManager>().ShowPanel()
        });

        _backBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<UIManager>().ShowPanel(ScreenType.Aquarium);
        });

        _addTankBtn.RegisterCallback<ClickEvent>(e => {

        });

        _addPlantBtn.RegisterCallback<ClickEvent>(e => {

        });
    }

    protected override void FindElement(VisualElement root)
    {
        _timeText = root.Q<Label>("time-text");
        _dateText = root.Q<Label>("date-text");
        _goldText = root.Q("money-container").Q<Label>("text");

        _settingBtn = root.Q<VisualElement>("setting-btn");
        _backBtn = root.Q<VisualElement>("back-btn");

        _addTankBtn = root.Q<VisualElement>("add-tank-btn");
        _addPlantBtn = root.Q<VisualElement>("add-plant-btn");
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
