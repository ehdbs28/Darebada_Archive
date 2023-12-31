using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UIElements;
using static Core.Define;

public class OceanScreen : UIScreen
{
    private Label _yearText;
    private Label _dateText;
    private Label _timeText;

    private VisualElement _backpackBtn;
    private VisualElement _gobackBtn;
    private VisualElement _letterBtn;
    private VisualElement _dictionaryBtn;
    private VisualElement _inventoryBtn;

    private VisualElement _challengeBtn;

    private UICompass _compass;
    private UIBoatFuel _boatDurability;
    private BoatControllerUI _controllerUI;

    [SerializeField]
    private Gradient _boatDurabilityGradient;

    private BoatController _boatController;

    public override void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true)
    {
        base.SetUp(document, clearScreen);
        _boatController = GameObject.Find("Boat").GetComponent<BoatController>();
        OnChangedDay(GameManager.Instance.GetManager<TimeManager>().DateTime);
    }

    private void Update() {
        if(GameManager.Instance.GetManager<UIManager>().ActiveScreen != ScreenType.Ocean || _boatController == null || _compass == null){
            return;
        }

        float theta = Mathf.Acos(Vector3.Dot(North, _boatController.BoatActionData.Forward)) * Mathf.Rad2Deg;
        
        if(Vector3.Cross(_boatController.BoatActionData.Forward, North).y < 0f){
            theta *= -1f;
        }

        _compass.SetRotate(theta);
    }

    public override void AddEvent()
    {
        GameManager.Instance.GetManager<TimeManager>().OnTimeChangedEvent += OnChangedTime;
        GameManager.Instance.GetManager<TimeManager>().OnDayChangedEvent += OnChangedDay;

        _backpackBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.ItemSelect);
        });

        _gobackBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            GameManager.Instance.GetManager<GameSceneManager>().ChangeScene(GameSceneType.Camp);
        });

        _letterBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.Letter);
        });

        _dictionaryBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.Dictionary);
        });

        _inventoryBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.Inventory);
        });

        _challengeBtn.RegisterCallback<ClickEvent>(e =>
        {
            GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.Challenge);
        });
    }

    public override void RemoveEvent()
    {
        GameManager.Instance.GetManager<TimeManager>().OnTimeChangedEvent -= OnChangedTime;
        GameManager.Instance.GetManager<TimeManager>().OnDayChangedEvent -= OnChangedDay;
        _controllerUI.RemoveEvent();
    }

    public override void FindElement()
    {
        _yearText = _root.Q<Label>("year");
        _dateText = _root.Q<Label>("date");
        _timeText = _root.Q<Label>("time");

        _backpackBtn = _root.Q<VisualElement>("backpack-btn");
        _gobackBtn = _root.Q<VisualElement>("goto-btn");
        _letterBtn = _root.Q<VisualElement>("letter-btn");
        _dictionaryBtn = _root.Q<VisualElement>("dictionary-btn");
        _inventoryBtn = _root.Q<VisualElement>("inventory-btn");

        _challengeBtn = _root.Q<VisualElement>("complete-box");

        VisualElement compassRoot = _root.Q<VisualElement>("compass");
        _compass = new UICompass(compassRoot);

        VisualElement boatDurabilityRoot = _root.Q<VisualElement>("boat-data");
        _boatDurability = new UIBoatFuel(boatDurabilityRoot, _boatDurabilityGradient, _boatController);

        VisualElement controllerRoot = _root.Q<VisualElement>("boat-controller");
        _controllerUI = new BoatControllerUI(controllerRoot, _boatController, 20f);
    }

    public void OnReduceBoatDurability(){
        _boatDurability.SetColor();
    }

    private void OnChangedTime(int hour, int minute, float currentTime){
        _timeText.text = $"{hour:D2}:{minute:D2}";
    }

    private void OnChangedDay(GameDate gamedate)
    {
        _yearText.text = $"{gamedate.Year}년째";
        _dateText.text = $"{gamedate.Month}월 {gamedate.Day}일";
    }
}
