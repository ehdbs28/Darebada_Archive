using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : IManager
{
    private GameSceneType _activeSceneType;
    public GameSceneType ActiveSceneType => _activeSceneType;

    private GameScene _activeScene = null;

    public AudioClip _bgmClip;

    public void ChangeScene(GameSceneType next){
        if(_activeScene != null){
            _activeScene.ExitScene();
            GameManager.Instance.GetManager<PoolManager>().Push(_activeScene);   
        }

        _activeScene = GameManager.Instance.GetManager<PoolManager>().Pop($"{next}Scene") as GameScene;
        GameManager.Instance.GetManager<SoundManager>().Stop();
        _activeScene?.EnterScene();
        _bgmClip = GameObject.Find($"{next}Scene").GetComponent<AudioSource>().clip;
        GameManager.Instance.GetManager<SoundManager>().Play(_bgmClip, SoundEnum.BGM);
    }

    public void InitManager(){}
    public void UpdateManager(){}
    public void ResetManager(){}
}
