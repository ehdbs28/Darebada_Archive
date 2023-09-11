using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UIElements;

public class AquariumEditScreen : UIScreen
{
    private Label _timeText;
    private Label _dateText;
    private Label _goldText;

    private VisualElement _backBtn;

    private VisualElement _addTankBtn;
    private VisualElement _addPlantBtn;
    private VisualElement _addRoadBtn;

    public override void AddEvent()
    {
        GameManager.Instance.GetManager<TimeManager>().OnTimeChangedEvent += OnChangedTime;
        GameManager.Instance.GetManager<TimeManager>().OnDayChangedEvent += OnChangedDay;

        GameManager.Instance.GetManager<InputManager>().OnTouchEvent += OnTouchHandle;

        _backBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            GameManager.Instance.GetManager<CameraManager>().SetVCam(CameraState.PLAYER_FOLLOW);
            GameManager.Instance.GetManager<UIManager>().ShowPanel(ScreenType.Aquarium);
        });

        _addTankBtn.RegisterCallback<ClickEvent>(e => {
            //GameManager.Instance.GetManager<SoundManager>().ClickSound();
            GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.TankUpgrade);
        });

        _addPlantBtn.RegisterCallback<ClickEvent>(e => {
            //GameManager.Instance.GetManager<SoundManager>().ClickSound();
            //GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.);
        });

        _addRoadBtn.RegisterCallback<ClickEvent>(e =>
        {
            //GameManager.Instance.GetManager<SoundManager>().ClickSound();
        });
    }

    public override void RemoveEvent()
    {
        GameManager.Instance.GetManager<TimeManager>().OnTimeChangedEvent -= OnChangedTime;
        GameManager.Instance.GetManager<TimeManager>().OnDayChangedEvent -= OnChangedDay;

        GameManager.Instance.GetManager<InputManager>().OnTouchEvent -= OnTouchHandle;
    }

    public override void FindElement()
    {
        _timeText = _root.Q<Label>("time-text");
        _dateText = _root.Q<Label>("date-text");
        _goldText = _root.Q("money-container").Q<Label>("text");

        _backBtn = _root.Q<VisualElement>("back-btn");

        _addTankBtn = _root.Q<VisualElement>("add-tank-btn");
        _addPlantBtn = _root.Q<VisualElement>("add-plant-btn");
    }

    private void OnChangedTime(int hour, int minute, float currentTime)
    {
        _timeText.text = $"{hour.ToString("D2")}:{minute.ToString("D2")}";
    }

    private void OnChangedDay(GameDate gameDate)
    {
        _dateText.text = $"{gameDate.Year}년째, {gameDate.Month}월{gameDate.Day}일";
    }

    private void OnTouchHandle(){
        Vector3 point = GameManager.Instance.GetManager<InputManager>().GetMouseRayPoint("Facility");

        if(point != Vector3.zero){
            GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.TankUpgrade);
        }
    }
}
