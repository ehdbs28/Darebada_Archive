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

    private UIDocument _document;
    private UIDocument _blurDocument;

    private ScreenType _activeScreen;
    private PopupType _activePopup;

    public ScreenType ActiveScreen => _activeScreen;
    public PopupType ActivePopup => _activePopup;

    public void InitManager()
    {
        _document =  GetComponent<UIDocument>();
        _blurDocument = GameObject.Find("Settings/Blur Screen/BlurUI").GetComponent<UIDocument>();

        Transform screenTrm = transform.Find("Screens");
        Transform popupTrm = transform.Find("Popups");

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
    }

    public void ShowPanel(ScreenType type, bool clearScreen = true){
        if(_screens[type] != null){
            _screens[type]?.SetUp(_document, clearScreen);
            _screens[type]?.SetUp(_blurDocument, clearScreen);
            _activeScreen = type;
        }
    }

    public void ShowPanel(PopupType type, bool clearScreen = false){
        if(_popups[type] != null && _popups[type].IsOpenPopup == false){
            _popups[type]?.SetUp(_document, clearScreen);
            _activePopup = type;
        }
    }

    public UIScreen GetPanel(ScreenType type){
        return _screens[type];
    }

    public UIPopup GetPanel(PopupType type){
        return _popups[type];
    }

    public bool OnElement(Vector3 screenPos){
        IPanel panel = _document.rootVisualElement.panel;

        Vector3 panelPos = RuntimePanelUtils.ScreenToPanel(panel, screenPos);
        VisualElement pick = panel.Pick(panelPos);

        return pick != null;
    }

    public void UpdateManager() {
        if(Input.GetKeyDown(KeyCode.B)){
            ShowPanel(PopupType.Shop);
        }
    }
    public void ResetManager() {}
}
