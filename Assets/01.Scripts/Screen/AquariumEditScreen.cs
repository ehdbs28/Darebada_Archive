using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UIElements;

public class AquariumEditScreen : UIScreen
{
    private Label _yearText;
    private Label _dateText;
    private Label _timeText;
    
    private Label _goldText;

    private VisualElement _backBtn;

    private VisualElement _addTankBtn;
    private VisualElement _addPlantBtn;
    private VisualElement _addRoadBtn;

    private AquariumEditVCam _editCam;

    private Transform _playerTrm;

    public override void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true)
    {
        base.SetUp(document, clearScreen, blur, timeStop);

            
        OnChangedDay(GameManager.Instance.GetManager<TimeManager>().DateTime);
        OnChangedGold((GameManager.Instance.GetManager<DataManager>().GetData(DataType.GameData) as GameData).HoldingGold);

        _editCam = (AquariumEditVCam)GameManager.Instance.GetManager<CameraManager>().SetVCam(CameraState.AQUARIUM_EDIT);
        _playerTrm = _editCam.transform.parent.Find("Player");
        var pos = _playerTrm.position - (new Vector3(1, 0, 1) * 5f);
        pos.y = 10f;
        _editCam.SetPosition(pos);
    }

    public void TouchHandleManaged(bool attach)
    {
        if (attach)
        {
            GameManager.Instance.GetManager<InputManager>().OnTouchEvent += OnTouchHandle;    
        }
        else
        {
            GameManager.Instance.GetManager<InputManager>().OnTouchEvent -= OnTouchHandle;
        }
    }

    public override void AddEvent()
    {
        GameManager.Instance.GetManager<TimeManager>().OnTimeChangedEvent += OnChangedTime;
        GameManager.Instance.GetManager<TimeManager>().OnDayChangedEvent += OnChangedDay;
        GameManager.Instance.GetManager<MoneyManager>().OnGoldChange += OnChangedGold;

        GameManager.Instance.GetManager<InputManager>().OnTouchEvent += OnTouchHandle;

        _backBtn.RegisterCallback<ClickEvent>(e =>
        {
            if(AquariumManager.Instance.state == AquariumManager.STATE.CAMMOVE)
            {
                AquariumManager.Instance.state = AquariumManager.STATE.NORMAL;
                GameManager.Instance.GetManager<SoundManager>().ClickSound();
                GameManager.Instance.GetManager<CameraManager>().SetVCam(CameraState.PLAYER_FOLLOW);
                GameManager.Instance.GetManager<UIManager>().ShowPanel(ScreenType.Aquarium);

            }else if(AquariumManager.Instance.state == AquariumManager.STATE.BUILD)
            {
                GameManager.Instance.GetManager<SoundManager>().ClickSound();
                AquariumManager.Destroy(AquariumManager.Instance.facilityObj.gameObject);
                AquariumManager.Instance.facilityObj = null;
                FindObjectOfType<GridManager>().HideGrid();
                AquariumManager.Instance.state = AquariumManager.STATE.CAMMOVE;
            }
        });

        _addTankBtn.RegisterCallback<ClickEvent>(e =>
        {
            if (AquariumManager.Instance.state == AquariumManager.STATE.BUILD)
                return;
            
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            TouchHandleManaged(false);
            AquariumManager.Instance.AddFishBowl();
        });

        _addPlantBtn.RegisterCallback<ClickEvent>(e => {
            if (AquariumManager.Instance.state == AquariumManager.STATE.BUILD)
                return;
            
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            TouchHandleManaged(false);
            AquariumManager.Instance.AddSnackShop();
        });

        _addRoadBtn.RegisterCallback<ClickEvent>(e =>
        {
            if (AquariumManager.Instance.state == AquariumManager.STATE.BUILD)
                return;
            
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            TouchHandleManaged(false);
            AquariumManager.Instance.AddRoadTile();
        });
    }

    public override void RemoveEvent()
    {
        GameManager.Instance.GetManager<TimeManager>().OnTimeChangedEvent -= OnChangedTime;
        GameManager.Instance.GetManager<TimeManager>().OnDayChangedEvent -= OnChangedDay;
        GameManager.Instance.GetManager<MoneyManager>().OnGoldChange -= OnChangedGold;

        GameManager.Instance.GetManager<InputManager>().OnTouchEvent -= OnTouchHandle;
    }

    public override void FindElement()
    {
        _yearText = _root.Q<Label>("year-text");
        _timeText = _root.Q<Label>("time-text");
        _dateText = _root.Q<Label>("date-text");
        _goldText = _root.Q("money-container").Q<Label>("text");

        _backBtn = _root.Q<VisualElement>("back-btn");

        _addTankBtn = _root.Q<VisualElement>("add-tank-btn");
        _addPlantBtn = _root.Q<VisualElement>("add-plant-btn");
        _addRoadBtn = _root.Q<VisualElement>("add-road-btn");
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

    private void OnTouchHandle(){
        if (AquariumManager.Instance.state == AquariumManager.STATE.CAMMOVE)
        {
            Vector3 point = GameManager.Instance.GetManager<InputManager>().GetMouseRayPoint(out var hit, "Facility");

            if(point != Vector3.zero){
                if (hit.collider.TryGetComponent<Fishbowl>(out var fishbowl))
                {
                    ((TankUpgradePopup)GameManager.Instance.GetManager<UIManager>().GetPanel(PopupType.TankUpgrade))
                        .SetFishBowl(fishbowl);
                    GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.TankUpgrade);
                }
            }
        }
    }
}
