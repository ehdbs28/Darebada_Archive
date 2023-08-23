using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : IManager
{
    private GameSceneType _activeSceneType;
    public GameSceneType ActiveSceneType => _activeSceneType;

    private GameScene _activeScene = null;

    public AudioClip _bgmClip;

    public void ChangeScene(GameSceneType next){
        if (_activeScene != null){
            _activeScene.ExitScene();
            GameManager.Instance.GetManager<PoolManager>().Push(_activeScene);   
        }
        if (!LoadingManager.instance._isLoading && LoadingManager.instance._isStart)
        {
            LoadingManager.instance.LoadingEndGoScene = next;
            LoadingManager.instance._isLoading = true;
            LoadingManager.instance._loadingSceneStayTime = Random.Range(5f, 20f);
            SceneManager.LoadScene(1); // 
            return;
        }


        next = LoadingManager.instance.LoadingEndGoScene;
            GameManager.print("hereo");

        _activeScene = GameManager.Instance.GetManager<PoolManager>().Pop($"{next}Scene") as GameScene;
        GameManager.Instance.GetManager<SoundManager>().Stop();
        _activeScene?.EnterScene();

        LoadingManager.instance._isStart = true;

        if (next != GameSceneType.Ocean)
        {
            _bgmClip = GameObject.Find($"{next}Scene").GetComponent<AudioSource>().clip;
            GameManager.Instance.GetManager<SoundManager>().Play(_bgmClip, SoundEnum.BGM);
        }
    }

    public void InitManager(){}
    public void UpdateManager(){}
    public void ResetManager(){}
}
