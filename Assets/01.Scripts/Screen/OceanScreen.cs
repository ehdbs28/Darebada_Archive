using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Core.Define;

public class OceanScreen : UIScreen
{
    private Label _timeText;
    private Label _dateText;

    private VisualElement _settingBtn;
    private VisualElement _gobackBtn;
    private VisualElement _letterBtn;
    private VisualElement _dictionaryBtn;
    private VisualElement _inventoryBtn;

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

        _settingBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.Setting);
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
    }

    public override void RemoveEvent()
    {
        GameManager.Instance.GetManager<TimeManager>().OnTimeChangedEvent -= OnChangedTime;
        GameManager.Instance.GetManager<TimeManager>().OnDayChangedEvent -= OnChangedDay;
        _controllerUI.RemoveEvent();
    }

    public override void FindElement()
    {
        _timeText = _root.Q<Label>("time-text");
        _dateText = _root.Q<Label>("date-text");

        _settingBtn = _root.Q<VisualElement>("setting-btn");
        _gobackBtn = _root.Q<VisualElement>("goto-btn");
        _letterBtn = _root.Q<VisualElement>("letter-btn");
        _dictionaryBtn = _root.Q<VisualElement>("dictionary-btn");
        _inventoryBtn = _root.Q<VisualElement>("inventory-btn");

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

    private void OnChangedTime(int hour, int minute){
        _timeText.text = $"{hour:D2}:{minute:D2}";
    }

    private void OnChangedDay(int year, int month, int day){
        _dateText.text = $"{year}년째, {month}월{day}일";
    }
}
