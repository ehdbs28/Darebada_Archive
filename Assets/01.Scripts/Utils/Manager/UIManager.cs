using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour, IManager
{
    private readonly Dictionary<ScreenType, UIScreen> _panels = new Dictionary<ScreenType, UIScreen>();

    private UIDocument _document;

    public void InitManager()
    {
        _document =  GetComponent<UIDocument>();
        Transform screenTrm = transform.Find("Screens");

        foreach(ScreenType type in Enum.GetValues(typeof(ScreenType))){
            UIScreen screen = screenTrm.GetComponent($"{type}Screen") as UIScreen;

            if(screen == null){
                Debug.LogError($"There is no script : {type}");
                return;
            }

            _panels.Add(type, screen);
        }

        foreach(ScreenType type in Enum.GetValues(typeof(PopupType))){
            UIPopup popup = screenTrm.GetComponent($"{type}Popup") as UIPopup;

            if(popup == null){
                Debug.LogError($"There is no script : {type}");
                return;
            }

            _panels.Add(type, popup);
        }

        // For debuging
        ChangeScreen(ScreenType.Ocene);
    }

    public void ChangeScreen(ScreenType type, bool clearScreen = true){
        _panels[type]?.SetUp(_document, clearScreen);
    }

    public UIScreen GetScreen(ScreenType type){
        return _panels[type];
    }

    public void UpdateManager() {}
    public void ResetManager() {}
}
