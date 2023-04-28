using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class VCam : MonoBehaviour
{
    public Action OnSwipeAction = null;

    protected CinemachineVirtualCamera _virtualCam;
    public CinemachineVirtualCamera VirtualCam => _virtualCam;

    public virtual void Init(){
        _virtualCam = GetComponent<CinemachineVirtualCamera>();
        _virtualCam.Priority = 0;

        OnSwipeAction += OnSwipeEvent;
    }

    public virtual void SelectVCam(){
        _virtualCam.Priority = 10;
    }

    public virtual void UnselectVCam(){
        _virtualCam.Priority = 0;
    }

    protected abstract void OnSwipeEvent();
}
