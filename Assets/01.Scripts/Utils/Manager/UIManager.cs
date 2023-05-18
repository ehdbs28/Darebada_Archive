using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour, IManager
{
    private readonly Dictionary<ScreenType, UIScreen> _screens = new Dictionary<ScreenType, UIScreen>();
    public Dictionary<ScreenType, UIScreen> Screens => _screens;

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

            _screens.Add(type, screen);
        }

        ChangeScreen(ScreenType.Ocene);
    }

    public void ChangeScreen(ScreenType type, bool clearScreen = true){
        _screens[type]?.SetUp(_document, clearScreen);
    }

    public void UpdateManager() {}
    public void ResetManager() {}
}
