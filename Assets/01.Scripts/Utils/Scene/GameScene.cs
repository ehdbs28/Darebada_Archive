using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameScene : PoolableMono
{   
    [Header("Scean Type")]
    [SerializeField]
    private GameSceneType _sceneType;

    [Header("InitScreen")]
    [SerializeField]
    private ScreenType _initScreenType;

    [Header("First Setting VCam")]
    [SerializeField]
    private CameraState _initCamState;
    [SerializeField]
    private CameraState _lastCamState;
    [SerializeField]
    private List<VCam> _vCamList = new List<VCam>();

    [SerializeField]
    private UnityEvent OnEnterScene = null, OnExitScene = null;

    public void EnterScene(){
        GameManager.Instance.GetManager<CameraManager>().SetVCamList(_vCamList);
        GameManager.Instance.GetManager<CameraManager>().SetVCam(_initCamState);

        GameManager.Instance.GetManager<UIManager>().ShowPanel(_initScreenType);
        
        OnEnterScene?.Invoke();
    }
    
    public void ExitScene(){
        OnExitScene?.Invoke();
    }

    public override void Init(){
        for(int i = (int)_initCamState; i < (int)_lastCamState; i++){
            _vCamList[i - (int)_initCamState].Init((CameraState)i);
        }
    }
}