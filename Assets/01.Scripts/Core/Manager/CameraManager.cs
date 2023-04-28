using System.Linq;
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
    private List<VCam> _virtualCams = new List<VCam>();

    private VCam _currentActiveVCam = null;

    private void Awake() {
        if(Instance == null){
            Instance = this;
        }

        _virtualCams.ForEach(cam => cam.Init());

        // OnSwipeEvent 나중에 인풋매니저 이벤트에 더해주기
    }

    private void Start() {
        SetVCam<BoatFollowingVCam>();
    }

    public void SetVCam<T>() where T : VCam{
        T virtualCam = null;

        foreach(T cam in _virtualCams.OfType<T>()){
            virtualCam = cam;
        }

        if(virtualCam == null) return;

        _currentActiveVCam?.UnselectVCam();
        _currentActiveVCam = virtualCam;
        _currentActiveVCam?.SelectVCam();
    }

    private void OnSwipeEvent(){
        _currentActiveVCam?.OnSwipeAction?.Invoke();
    }
}
