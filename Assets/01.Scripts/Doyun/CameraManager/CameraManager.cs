using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("카메라 기초 세팅 값")]
    [SerializeField] private float _camBorderThickness = 10f;

    [Header("카메라 움직임 관련 변수들")]
    [SerializeField] private float _camRotateSpeed = 5f;

    private Camera _mainCam = null;
    public Camera MainCam{
        get{
            if(_mainCam == null)
                _mainCam = Camera.main;

            return _mainCam;
        }
    }

    private void Update()
    {
        CameraRotate(Input.mousePosition);
    }

    private void CameraRotate(Vector3 mousePos)
    {
        if (mousePos.y > Screen.height - _camBorderThickness)
            MainCam.transform.rotation = Quaternion.Lerp(MainCam.transform.rotation, Quaternion.Euler(20f, -45f, 0f), Time.deltaTime);
        else if (mousePos.y < _camBorderThickness)
            MainCam.transform.rotation = Quaternion.Lerp(MainCam.transform.rotation, Quaternion.Euler(65f, -45f, 0f), Time.deltaTime);

        if (mousePos.x > Screen.width - _camBorderThickness)
            MainCam.transform.rotation = Quaternion.Lerp(MainCam.transform.rotation, Quaternion.Euler(45f, -20f, 0f), Time.deltaTime);
        else if (mousePos.x < _camBorderThickness)
            MainCam.transform.rotation = Quaternion.Lerp(MainCam.transform.rotation, Quaternion.Euler(45f, -65f, 0f), Time.deltaTime);
    }
}
