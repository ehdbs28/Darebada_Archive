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

        GameManager.Instance.GetManager<InputManager>().OnTouchEvent += OnScreenClick;
    }

    public override void UnselectVCam()
    {
        base.UnselectVCam();

        GameManager.Instance.GetManager<InputManager>().OnTouchEvent -= OnScreenClick;
    }

    public override void UpdateCam()
    {
    }

    private void OnScreenClick(){
        GameManager.Instance.GetManager<CameraManager>().SetRotateCam(
            _target,
            Vector3.Distance(_target.position, _virtualCam.transform.position),
            _virtualCam.transform.position,
            _virtualCam.transform.rotation,
            Vector3.zero,
            _state
        );
    }
}