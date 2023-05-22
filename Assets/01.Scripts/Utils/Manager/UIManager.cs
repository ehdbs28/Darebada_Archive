using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour, IManager
{
    private readonly Dictionary<ScreenType, UIScreen> _screens = new Dictionary<ScreenType, UIScreen>();
    private readonly Dictionary<PopupType, UIScreen> _popups = new Dictionary<PopupType, UIScreen>();

    private UIDocument _document;

    public void InitManager()
    {
        _document =  GetComponent<UIDocument>();
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

        // For debuging
        ShowPanel(ScreenType.Ocene);
    }

    public void ShowPanel(ScreenType type, bool clearScreen = true){
        _screens[type]?.SetUp(_document, clearScreen);
    }

    public void ShowPanel(PopupType type, bool clearScreen = false){
        _popups[type]?.SetUp(_document, clearScreen);
    }

    public UIScreen GetPanel(ScreenType type){
        return _screens[type];
    }

    public UIScreen GetPanel(PopupType type){
        return _popups[type];
    }

    public void UpdateManager() {}
    public void ResetManager() {}
}
