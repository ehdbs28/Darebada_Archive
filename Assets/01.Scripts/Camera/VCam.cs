using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Core;

public abstract class VCam : MonoBehaviour
{
    protected CinemachineVirtualCamera _virtualCam;
    public CinemachineVirtualCamera VirtualCam => _virtualCam;

    protected CameraState _state;
    public CameraState State => _state;

    public virtual void Init(CameraState state){
        _virtualCam = GetComponent<CinemachineVirtualCamera>();
        _virtualCam.Priority = 0;
        _state = state;
    }

    public virtual void SelectVCam(){
        _virtualCam.Priority = 10;
    }

    public virtual void UnselectVCam(){
        _virtualCam.Priority = 0;
    }

    public abstract void UpdateCam();
}
