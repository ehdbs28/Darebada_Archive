using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour, IManager
{
    private VCam _currentActiveVCam = null;

    [SerializeField]
    private VCam _rotateVCam;

    private List<VCam> _virtualCams = new List<VCam>();

    public void InitManager() {
        _rotateVCam.Init(CameraState.ROTATE);
    }

    public void UpdateManager() {
        _currentActiveVCam?.UpdateCam();
    }

    public VCam SetVCam(CameraState state){
        VCam virtualCam = null;

        foreach(var vCam in _virtualCams.Where(vCam => vCam.State == state)){
            virtualCam = vCam;
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

    public void SetVCamList(List<VCam> vCamList){
        _virtualCams = vCamList;
        _virtualCams.Add(_rotateVCam);
    }

    public void ResetManager(){}
}
