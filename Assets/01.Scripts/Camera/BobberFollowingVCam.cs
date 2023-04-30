using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberFollowingVCam : VCam
{
    [SerializeField]
    private Transform _target;

    public override void SelectVCam()
    {
        base.SelectVCam();

        InputManager.Instance.OnMouseClickEvent += OnScreenClick;
    }

    public override void UnselectVCam()
    {
        base.UnselectVCam();

        InputManager.Instance.OnMouseClickEvent -= OnScreenClick;
    }

    public override void UpdateCam()
    {
    }

    private void OnScreenClick(bool value){
        if(value == true){
            CameraManager.Instance.SetRotateCam(_target,
                                                Vector3.Distance(_target.position, _virtualCam.transform.position),
                                                _virtualCam.transform.position,
                                                _virtualCam.transform.rotation,
                                                Vector3.zero,
                                                _state
            );
        }
    }
}