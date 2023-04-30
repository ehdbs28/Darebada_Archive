using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class RotationVCam : VCam
{
    private Arcball _arcball = null;

    private Vector3 _last;
    private Vector3 _spherical;
    [SerializeField] private Vector3 _offset;

    [SerializeField]
    private float _rotationSpeed; 
    private float _radius;
    
    private bool _isRotate = false;

    private Core.CameraState _lastVCamState;

    public void SetRotateValue(Transform target, float radius, Vector3 pos, Quaternion rot, Vector3 offset, Core.CameraState lastVCamState){
        _last = InputManager.Instance.MousePosition;
        _lastVCamState = lastVCamState;
        _offset = offset;
        _arcball = new Arcball(radius, target);

        _virtualCam.transform.position = pos;
        _virtualCam.transform.rotation = rot;
        _spherical = _arcball.GetSphericalCoordinates(_virtualCam.transform.position);

        InputManager.Instance.OnMouseClickEvent += OnMouseClick;

        _isRotate = true;
    }

    public override void UnselectVCam()
    {
        base.UnselectVCam();
        InputManager.Instance.OnMouseClickEvent -= OnMouseClick;
        _isRotate = false;
    }

    public override void UpdateCam()
    {
        if(_isRotate){
            float dx = (_last.x - InputManager.Instance.MousePosition.x) * _rotationSpeed;

            if(dx != 0f){
                _spherical.y += dx * Time.deltaTime;

                // 여기서 카메라 돌려주기
                _virtualCam.transform.position = _arcball.GetCartesianCoordinates(_spherical) + _arcball.Center;

                // 타겟을 바라보게
                _virtualCam.transform.rotation = Quaternion.LookRotation(_arcball.Center + _offset - _virtualCam.transform.position);
            }

            _last = InputManager.Instance.MousePosition;
        }
    }

    private void OnMouseClick(bool value){
        if(value == false){
            CameraManager.Instance.SetVCam(_lastVCamState);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        if(UnityEditor.Selection.activeGameObject == gameObject && _arcball != null){
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_arcball.Center, _arcball.Radius);
        }
    }
#endif
}
