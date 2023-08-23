using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    private static LoadingManager _instance;
    public static LoadingManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(LoadingManager)) as LoadingManager;
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<LoadingManager>();
                }
            }

            return _instance;
        }
    }

    private bool _isLoading = false;
    private bool _isStart = false;
    
    private GameSceneType _loadingEndGoScene;
    public GameSceneType LoadingEndGoScene
    {
        get { return _loadingEndGoScene; }
        set { _loadingEndGoScene = value; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (!_isStart)
        {
            _loadingEndGoScene = GameSceneType.Camp;
        }
    }

    public void OnLoading(GameSceneType moveSceneType)
    {
        if (!_isLoading && _isStart)
        {
            _loadingEndGoScene = moveSceneType;
            SceneManager.LoadScene(1); // ¹Ù²ã¾ßÇÔ
            return;
        }

        _isStart = true;
    }

    public void SetLoading(bool value)
    {
        _isLoading = value;
    }
}