using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BoatFollowingVCam : VCam
{
    [SerializeField]
    private Transform _target;

    public override void SelectVCam()
    {
        base.SelectVCam();

        GameManager.Instance.GetManager<InputManager>().OnMouseClickEvent += OnScreenClick;
    }

    public override void UnselectVCam()
    {
        base.UnselectVCam();

        GameManager.Instance.GetManager<InputManager>().OnMouseClickEvent -= OnScreenClick;
    }

    public override void UpdateCam()
    {
    }

    private void OnScreenClick(bool value){
        if(value == true){
            CinemachineFramingTransposer framingTransposer = ((CinemachineVirtualCamera)_virtualCam).GetCinemachineComponent<CinemachineFramingTransposer>();
            Vector3 offset = framingTransposer.m_TrackedObjectOffset;

            GameManager.Instance.GetManager<CameraManager>().SetRotateCam(
                _target,
                Vector3.Distance(_target.position, _virtualCam.transform.position),
                _virtualCam.transform.position,
                _virtualCam.transform.rotation,
                new Vector3(0f, offset.y, 0f),
                _state
            );
        }
    }
}
