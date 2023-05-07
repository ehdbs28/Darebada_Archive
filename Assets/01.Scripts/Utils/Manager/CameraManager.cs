using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using static Core.CameraState;

public class CameraManager : MonoBehaviour, IManager
{
    [SerializeField]
    private List<VCam> _virtualCamList = new List<VCam>();

    private VCam _currentActiveVCam = null;

    private readonly Dictionary<Core.CameraState, VCam> _virtualVCams = new Dictionary<Core.CameraState, VCam>();

    public CameraManager(){
        ResetManager();
    }

    public void InitManager() {
        for(int i = 0; i < (int)FINISH; i++){
            _virtualCamList[i].Init((Core.CameraState)i);
            _virtualVCams.Add((Core.CameraState)i, _virtualCamList[i]);
        }

        SetVCam(Core.CameraState.BOAT_FOLLOW);
    }

    public void UpdateManager() {
        _currentActiveVCam?.UpdateCam();
    }

    public VCam SetVCam(Core.CameraState state){
        VCam virtualCam = null;

        foreach(var key in _virtualVCams.Keys.Where(key => key == state)){
            virtualCam = _virtualVCams[key];
        }

        if(virtualCam == null) return null;

        _currentActiveVCam?.UnselectVCam();
        _currentActiveVCam = virtualCam;
        _currentActiveVCam?.SelectVCam();

        return virtualCam;
    }

    public void SetRotateCam(Transform target, float radius, Vector3 pos, Quaternion rot, Vector3 offset, Core.CameraState lastState){
        RotationVCam rotVCam = (RotationVCam)SetVCam(Core.CameraState.ROTATE);
        rotVCam.SetRotateValue(target, radius, pos, rot, offset, lastState);
    }

    public void ResetManager(){}
}
