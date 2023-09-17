using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberFollowingVCam : VCam
{
    [SerializeField]
    private FishingController _fishingController;

    [SerializeField]
    private FishingState _targetState;
    
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
        if (_fishingController.CurrentState != _targetState)
            return;

        GameManager.Instance.GetManager<CameraManager>().SetRotateCam(
            _target,
            Vector3.Distance(_target.position, _virtualCam.transform.position),
            _virtualCam.transform.position,
            _virtualCam.transform.rotation,
            Vector3.zero,
            _state,
            Vector3.zero
        );
    }
}