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

        next = GameManager.Instance.GetManager<LoadingManager>().OnLoading(next);
        
        _activeScene = GameManager.Instance.GetManager<PoolManager>().Pop($"{next}Scene") as GameScene;
        GameManager.Instance.GetManager<SoundManager>().Stop();
        _activeScene?.EnterScene();
 
        if (next != GameSceneType.Ocean)
        {
            if (next == GameSceneType.Loading) return;

            _bgmClip = _activeScene.GetComponent<AudioSource>().clip;
            GameManager.Instance.GetManager<SoundManager>().Play(_bgmClip, SoundEnum.BGM);
        }

        if (next == GameSceneType.Loading || next == GameSceneType.Camp) return;

        GameManager.Instance.GetManager<TutorialManager>().OnClickEvent(next);
    }

    public void InitManager(){}
    public void UpdateManager(){}
    public void ResetManager(){}
}
