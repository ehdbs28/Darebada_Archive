using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameScene : MonoBehaviour
{
    protected GameSceneType _sceneType;
    public GameSceneType SceneType => _sceneType;

    
}
