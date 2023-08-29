using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationVCam : VCam
{
    private Arcball _arcball = null;

    private Vector3 _last;
    private Vector3 _spherical;
    private Vector3 _offset;

    [SerializeField]
    private float _rotationSpeed; 
    private float _radius;
    
    private bool _isRotate = false;

    private CameraState _lastVCamState;

    public void SetRotateValue(Transform target, float radius, Vector3 pos, Quaternion rot, Vector3 offset, CameraState lastVCamState){
        _last = GameManager.Instance.GetManager<InputManager>().TouchPosition;
        _lastVCamState = lastVCamState;
        _offset = offset;
        _arcball = new Arcball(radius, target);

        _virtualCam.transform.position = pos;
        _virtualCam.transform.rotation = rot;
        _spherical = _arcball.GetSphericalCoordinates(_virtualCam.transform.position);

        GameManager.Instance.GetManager<InputManager>().OnTouchUpEvent += OnTouchUp;

        _isRotate = true;
    }

    public override void UnselectVCam()
    {
        base.UnselectVCam();
        GameManager.Instance.GetManager<InputManager>().OnTouchUpEvent -= OnTouchUp;
        _isRotate = false;
    }

    public override void UpdateCam()
    {
        if(_isRotate){
            float dx = (_last.x - GameManager.Instance.GetManager<InputManager>().TouchPosition.x) * _rotationSpeed;

            if(dx != 0f){
                _spherical.y += dx * Time.deltaTime;

                _virtualCam.transform.position = _arcball.Center + _arcball.GetCartesianCoordinates(_spherical);
                _virtualCam.transform.rotation = Quaternion.LookRotation(_arcball.Center + _offset - _virtualCam.transform.position);
            }

            _last = GameManager.Instance.GetManager<InputManager>().TouchPosition;
        }
    }

    private void OnTouchUp(){
        GameManager.Instance.GetManager<CameraManager>().SetVCam(_lastVCamState);
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
