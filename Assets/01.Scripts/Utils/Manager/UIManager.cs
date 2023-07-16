using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour, IManager
{
    private readonly Dictionary<ScreenType, UIScreen> _screens = new Dictionary<ScreenType, UIScreen>();
    private readonly Dictionary<PopupType, UIPopup> _popups = new Dictionary<PopupType, UIPopup>();
    private readonly Dictionary<UGUIType, UGUIPopup> _uguis = new Dictionary<UGUIType, UGUIPopup>();

    private UIDocument _document;
    private UIDocument _blurDocument;

    private ScreenType _activeScreen;
    private PopupType _activePopup;
    private UGUIType _activeUGUI;

    public ScreenType ActiveScreen => _activeScreen;
    public PopupType ActivePopup => _activePopup;
    public UGUIType ActiveUGUI => _activeUGUI;

    public void InitManager()
    {
        _document =  GetComponent<UIDocument>();
        _blurDocument = GameObject.Find("Settings/Blur Screen/BlurUI").GetComponent<UIDocument>();

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

    public void ShowPanel(ScreenType type, bool clearScreen = true){
        _screens[_activeScreen].RemoveEvent();

        if(_screens[type] != null){
            _screens[type]?.SetUp(_blurDocument, clearScreen);
            _screens[type]?.SetUp(_document, clearScreen);
            _activeScreen = type;
        }
    }

    public void ShowPanel(PopupType type, bool clearScreen = false, bool blur = true, bool timeStop = true){
        if(_popups[type] != null && _popups[type].IsOpenPopup == false){
            _popups[type]?.SetUp(_document, clearScreen, blur, timeStop);
            _activePopup = type;
        }
    }

     public void ShowPanel(UGUIType type, bool clearScreen = false, bool blur = true, bool timeStop = true){
        if(_uguis[type] != null && _uguis[type].IsOpenPopup == false){
            _uguis[type]?.SetUp(_document, clearScreen, blur, timeStop);
            _activeUGUI = type;
        }
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

    public bool OnElement(Vector3 screenPos){
        IPanel panel = _document.rootVisualElement.panel;

        Vector3 panelPos = RuntimePanelUtils.ScreenToPanel(panel, screenPos);
        VisualElement pick = panel.Pick(panelPos);

        return pick != null;
    }

    public void UpdateManager() {}
    public void ResetManager() {}
}
