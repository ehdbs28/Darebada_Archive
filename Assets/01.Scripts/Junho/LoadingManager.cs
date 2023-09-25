using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LoadingManager : IManager
{
    private bool _isLoading = false;
    public bool IsLoading 
    {
        get { return _isLoading; }
        set { _isLoading = value; }
    }

    private bool _isStart = false;
    public bool IsStart => _isStart;

    public GameSceneType next;

    private VisualElement _root;

    public GameSceneType OnLoading(GameSceneType moveSceneType)
    {
        if (!_isLoading && _isStart)
        {
            next = moveSceneType;
            _root.visible = false;
            return GameSceneType.Loading;
        }
        else
        {
            _isStart = true;
            _root.visible = true;
            return moveSceneType;
        }
    }

    public void InitManager()
    {
        _root = GameManager.Instance.GetManager<UIManager>().Document.rootVisualElement.Q("main-container");
    }
    
    public void ResetManager(){}
    public void UpdateManager(){}
}