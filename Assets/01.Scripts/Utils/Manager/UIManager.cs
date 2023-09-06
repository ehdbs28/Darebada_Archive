using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour, IManager
{
    private readonly Dictionary<ScreenType, UIScreen> _screens = new Dictionary<ScreenType, UIScreen>();
    private readonly Dictionary<PopupType, UIPopup> _popups = new Dictionary<PopupType, UIPopup>();
    private readonly Dictionary<UGUIType, UGUIPopup> _uguis = new Dictionary<UGUIType, UGUIPopup>();

    private UIDocument _document;
    public UIDocument Document
    { 
        get { return _document; }
        set { _document = value; }
    }
    private UIDocument _blurDocument;
    
    [SerializeField]
    private Canvas _canvas;
    public Canvas Canvas => _canvas;
    
    [SerializeField]
    private RectTransform _destinationRectTrm;
    public RectTransform DestinationRectTrm => _destinationRectTrm;

    private ScreenType _activeScreen;
    private PopupType _activePopup;
    private UGUIType _activeUGUI;

    public ScreenType ActiveScreen => _activeScreen;
    public PopupType ActivePopup => _activePopup;
    public UGUIType ActiveUGUI => _activeUGUI;

    public void InitManager()
    {
        _document =  GetComponent<UIDocument>();
        //_blurDocument = GameObject.Find("Settings/Blur Screen/BlurUI").GetComponent<UIDocument>();

        Transform screenTrm = transform.Find("Screens");
        Transform popupTrm = transform.Find("Popups");
        Transform uguiTrm = transform.Find("UGUIPopups");

        foreach(ScreenType type in Enum.GetValues(typeof(ScreenType))){
            UIScreen screen = screenTrm.GetComponent($"{type}Screen") as UIScreen;

            if(screen == null){
                Debug.LogError($"There is no script : {type}");
                return;
            }

            _screens.Add(type, screen);
        }

        foreach(PopupType type in Enum.GetValues(typeof(PopupType))){
            UIPopup popup = popupTrm.GetComponent($"{type}Popup") as UIPopup;

            if(popup == null){
                Debug.LogError($"There is no script : {type}");
                return;
            }

            _popups.Add(type, popup);
        }

        foreach(UGUIType type in Enum.GetValues(typeof(UGUIType))){
            UGUIPopup popup = uguiTrm.GetComponent($"{type}Popup") as UGUIPopup;

            if(popup == null){
                Debug.LogError($"There is no script : {type}");
                return;
            }

            _uguis.Add(type, popup);
        }
    }

    public UIScreen ShowPanel(ScreenType type, bool clearScreen = true){
        _screens[_activeScreen].RemoveEvent();

        if(_screens[type] != null){
            //_screens[type]?.SetUp(_blurDocument, clearScreen);
            _screens[type]?.SetUp(_document, clearScreen);
            _activeScreen = type;
            return _screens[type];
        }

        return null;
    }

    public UIPopup ShowPanel(PopupType type, bool clearScreen = false, bool blur = true, bool timeStop = true){
        if(_popups[type] != null && _popups[type].IsOpenPopup == false){
            _popups[type]?.SetUp(_document, clearScreen, blur, timeStop);
            _activePopup = type;
            return _popups[type];
        }

        return null;
    }

     public UGUIPopup ShowPanel(UGUIType type, bool clearScreen = false, bool blur = false, bool timeStop = false){
        if(_uguis[type] != null && _uguis[type].IsOpenPopup == false){
            _uguis[type].SetUp(_document, clearScreen, blur, timeStop);
            _activeUGUI = type;
            return _uguis[type];
        }

        return null;
     }

    public UIScreen GetPanel(ScreenType type){
        return _screens[type];
    }

    public UIPopup GetPanel(PopupType type){
        return _popups[type];
    }

    public UGUIPopup GetPanel(UGUIType type){
        return _uguis[type];
    }

    public bool OnElement(Vector2 screenPos){
        IPanel panel = _document.rootVisualElement.panel;

        screenPos.y = Define.ScreenSize.y - screenPos.y;

        Vector2 panelPos = RuntimePanelUtils.ScreenToPanel(panel, screenPos);
        VisualElement pick = panel.Pick(panelPos);

        return pick != null;
    }

    public Vector2 GetElementPos(VisualElement element, Vector2 pivot = default(Vector2))
    {
        Vector2 elementPos = element.worldBound.position;
        elementPos.x += element.worldBound.width * pivot.x;
        elementPos.y += element.worldBound.height * pivot.y;
        
        return elementPos;
    }

    public void UpdateManager() {}
    public void ResetManager() {}
}
