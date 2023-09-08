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
    public VCam RotateVCam => _rotateVCam;
    public bool _isCanRotate = true;
    public bool _isStopRotate = false;

    private List<VCam> _virtualCams = new List<VCam>();

    public void InitManager() {
        _rotateVCam.Init(CameraState.ROTATE);
    }

    public void UpdateManager() {
        _currentActiveVCam?.UpdateCam();
        
        if (_currentActiveVCam != _rotateVCam) return;
        GameManager.Instance.GetManager<InputManager>().OnTouchUpEvent += (() => _isCanRotate = false);
        GameManager.Instance.GetManager<InputManager>().OnTouchEvent += (() =>
        {
            if (_isStopRotate) return;
            _isCanRotate = true;
        });

        ((RotateCam)_currentActiveVCam).SetCanRotate(_isCanRotate ? 300 : 0);
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

    public void SetRotateCam(Transform target, float radius, Vector3 pos, Quaternion rot, Vector3 offset, CameraState lastState, Vector3 viewPos){
        if (!_isCanRotate) return;
        _currentActiveVCam?.UnselectVCam();
        _currentActiveVCam = _rotateVCam;
        _currentActiveVCam?.SelectVCam();

        ((RotateCam)_currentActiveVCam).SetCamValue(target);
        ((RotateCam)_currentActiveVCam).SetCamPos(viewPos.y, 10);
        //((RotationVCam)_currentActiveVCam).SetRotateValue(target, radius, pos, rot, offset, lastState);
    }

    public void SetVCamList(List<VCam> vCamList){
        _virtualCams = vCamList;
    }

    public void ResetManager(){}
}
