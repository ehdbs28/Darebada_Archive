using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public GameSceneType OnLoading(GameSceneType moveSceneType)
    {
        if (!_isLoading && _isStart)
        {
            next = moveSceneType;
            return GameSceneType.Loading;
        }
        else
        {
            _isStart = true;
            return moveSceneType;
        }
    }

    public void ResetManager(){}
    public void InitManager(){}
    public void UpdateManager(){}
}