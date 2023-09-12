using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UIElements;

public class AquariumScreen : UIScreen
{
    private Label _yearText;
    private Label _dateText;
    private Label _timeText;
    
    private Label _goldText;

    private VisualElement _settingBtn;
    private VisualElement _letterBtn;
    private VisualElement _comebackBtn;
    private VisualElement _manageBtn;
    private VisualElement _editorBtn;

    public FishDataUnit DataUnit;

    public override void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true)
    {
        base.SetUp(document, clearScreen, blur, timeStop);
        OnChangedDay(GameManager.Instance.GetManager<TimeManager>().DateTime);
    }

    public override void AddEvent()
    {
        GameManager.Instance.GetManager<TimeManager>().OnTimeChangedEvent += OnChangedTime;
        GameManager.Instance.GetManager<TimeManager>().OnDayChangedEvent += OnChangedDay;
        GameManager.Instance.GetManager<MoneyManager>().OnGoldChange += OnChangedGold;

        _settingBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.Setting);
        });

        _letterBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.Letter);
        });

        _comebackBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            GameManager.Instance.GetManager<GameSceneManager>().ChangeScene(GameSceneType.Camp);
        });

        _manageBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.AquariumManage);
        });

        _editorBtn.RegisterCallback<ClickEvent>(e =>
        {
            AquariumManager.Instance.state = AquariumManager.STATE.CAMERAMOVE;
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            GameManager.Instance.GetManager<UIManager>().ShowPanel(ScreenType.AquariumEdit);
        });
    }

    public override void RemoveEvent()
    {
        GameManager.Instance.GetManager<TimeManager>().OnTimeChangedEvent -= OnChangedTime;
        GameManager.Instance.GetManager<TimeManager>().OnDayChangedEvent -= OnChangedDay;
    }

    public override void FindElement()
    {
        _yearText = _root.Q<Label>("year-text");
        _timeText = _root.Q<Label>("time-text");
        _dateText = _root.Q<Label>("date-text");
        _goldText = _root.Q("money-container").Q<Label>("text");

        _settingBtn = _root.Q<VisualElement>("setting-btn");
        _letterBtn = _root.Q<VisualElement>("letter-btn");
        //
        _comebackBtn = _root.Q<VisualElement>("dictionary-btn");
        _manageBtn = _root.Q<VisualElement>("manage-btn");
        _editorBtn = _root.Q<VisualElement>("editor-btn");
    }

    private void OnChangedTime(int hour, int minute, float currentTime)
    {
        _timeText.text = $"{hour.ToString("D2")}:{minute.ToString("D2")}";
    }

    private void OnChangedDay(GameDate gameDate)
    {
        _yearText.text = $"{gameDate.Year}년째";
        _dateText.text = $"{gameDate.Month}월{gameDate.Day}일";
    }

    private void OnChangedGold(int gold)
    {
        _goldText.text = $"${gold:N}";
    }
}
