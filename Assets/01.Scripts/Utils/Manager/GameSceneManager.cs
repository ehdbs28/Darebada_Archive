using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : IManager
{
    private GameSceneType _activeSceneType;
    public GameSceneType ActiveSceneType => _activeSceneType;

    private GameScene _activeScene = null;

    public void ChangeScene(GameSceneType next){
        if(_activeScene != null){
            _activeScene.ExitScene();
            GameManager.Instance.GetManager<PoolManager>().Push(_activeScene);   
        }

        _activeScene = GameManager.Instance.GetManager<PoolManager>().Pop($"{next}Scene") as GameScene;
        GameManager.Instance.GetManager<SoundManager>().Stop();
        _activeScene?.EnterScene();
    }

    public void InitManager(){}
    public void UpdateManager(){}
    public void ResetManager(){}
}
