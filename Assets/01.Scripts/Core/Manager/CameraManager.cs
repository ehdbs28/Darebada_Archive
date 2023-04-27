using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance = null;
    // 나중에 게임 매니저에서 인스턴스 생성해줄거임
    // 일단 지금은 Awake에서 하자

    [SerializeField]
    private CinemachineVirtualCamera _boatFollowVCam;

    [SerializeField]
    private CinemachineVirtualCamera _bobberFollowVCam;

    private CinemachineVirtualCamera _currentActiveVCam = null;

    private void Awake() {
        if(Instance == null){
            Instance = this;
        }
    }

    private void Start() {
        Init();
    }

    private void Init(){
        _currentActiveVCam = _boatFollowVCam;
        SetBoatVCam();
    }

    public void SetBoatVCam(){
        _currentActiveVCam.Priority = 0;
        _currentActiveVCam = _boatFollowVCam;
        _currentActiveVCam.Priority = 10;
    }

    public void SetBobberVCam(){
        _currentActiveVCam.Priority = 0;
        _currentActiveVCam = _bobberFollowVCam;
        _currentActiveVCam.Priority = 10;
    }
}
