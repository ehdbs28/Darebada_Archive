using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static Core.CameraState;

public class BoatFollowingVCam : VCam
{
    [SerializeField]
    private Transform _target;

    public override void SelectVCam()
    {
        base.SelectVCam();

        InputManager.Instance.OnMouseClickEvent += OnMouseClick;
    }

    public override void UnselectVCam()
    {
        base.UnselectVCam();

        InputManager.Instance.OnMouseClickEvent -= OnMouseClick;
    }

    public override void UpdateCam()
    {
    }

    private void OnMouseClick(bool value){
        if(value == true){
            CinemachineFramingTransposer framingTransposer = _virtualCam.GetCinemachineComponent<CinemachineFramingTransposer>();
            Vector3 offset = framingTransposer.m_TrackedObjectOffset;

            CameraManager.Instance.SetRotateCam(_target,
                                                Vector3.Distance(_target.position, _virtualCam.transform.position),
                                                _virtualCam.transform.position,
                                                _virtualCam.transform.rotation,
                                                new Vector3(0f, offset.y, 0f),
                                                _state
            );
        }
    }
}
