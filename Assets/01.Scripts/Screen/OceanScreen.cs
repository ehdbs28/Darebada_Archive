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

    [SerializeField]
    private Gradient _boatDurabilityGradient;

    private BoatController _boatController;

    public override void SetUp(UIDocument document, bool clearScreen = true)
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

    protected override void AddEvent(VisualElement root)
    {
        GameManager.Instance.GetManager<TimeManager>().OnTimeChangedEvent += OnChangedTime;
        GameManager.Instance.GetManager<TimeManager>().OnDayChangedEvent += OnChangedDay;

        _settingBtn.RegisterCallback<ClickEvent>(e => {
            Debug.Log("설정");
        });

        _gobackBtn.RegisterCallback<ClickEvent>(e => {
            Debug.Log("돌아가기");
        });

        _letterBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.Letter);
        });

        _dictionaryBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.Dictionary);
        });

        _inventoryBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.Inventory);
        });
    }

    public override void RemoveEvent()
    {
        GameManager.Instance.GetManager<TimeManager>().OnTimeChangedEvent -= OnChangedTime;
        GameManager.Instance.GetManager<TimeManager>().OnDayChangedEvent -= OnChangedDay;
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

        VisualElement compassRoot = root.Q<VisualElement>("compass");
        _compass = new UICompass(compassRoot);

        VisualElement boatDurabilityRoot = root.Q<VisualElement>("boat-data");
        _boatDurability = new UIBoatFuel(boatDurabilityRoot, _boatDurabilityGradient, _boatController);
    }

    public void OnReduceBoatDurability(){
        _boatDurability.SetColor();
    }

    private void OnChangedTime(int hour, int minute){
        _timeText.text = $"{hour.ToString("D2")}:{minute.ToString("D2")}";
    }

    private void OnChangedDay(int year, int month, int day){
        _dateText.text = $"{year}년째, {month}월{day}일";
    }
}
