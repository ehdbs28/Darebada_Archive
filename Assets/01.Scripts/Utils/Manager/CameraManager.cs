using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour, IManager
{
    [SerializeField]
    private List<VCam> _virtualCamList = new List<VCam>();

    private VCam _currentActiveVCam = null;

    private readonly Dictionary<CameraState, VCam> _virtualVCams = new Dictionary<CameraState, VCam>();

    public CameraManager(){
        ResetManager();
    }

    public void InitManager() {
        for(int i = 0; i < (int)CameraState.FINISH; i++){
            _virtualCamList[i].Init((CameraState)i);
            _virtualVCams.Add((CameraState)i, _virtualCamList[i]);
        }

        SetVCam(CameraState.BOAT_FOLLOW);
    }

    public void UpdateManager() {
        _currentActiveVCam?.UpdateCam();
    }

    public VCam SetVCam(CameraState state){
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

    public void SetRotateCam(Transform target, float radius, Vector3 pos, Quaternion rot, Vector3 offset, CameraState lastState){
        RotationVCam rotVCam = (RotationVCam)SetVCam(CameraState.ROTATE);
        rotVCam.SetRotateValue(target, radius, pos, rot, offset, lastState);
    }

    public void ResetManager(){}
}
