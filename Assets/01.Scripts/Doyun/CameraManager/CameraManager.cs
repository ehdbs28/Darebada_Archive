using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance = null;

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

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            CameraRotate(Input.mousePosition);

        if (MainCam.transform.rotation.x < -30)
            MainCam.transform.rotation = Quaternion.Euler(0, MainCam.transform.rotation.y, 0);
        if (MainCam.transform.rotation.x > 70)
            MainCam.transform.rotation = Quaternion.Euler(70, MainCam.transform.rotation.y, 0);

        if (MainCam.transform.rotation.y < -90)
            MainCam.transform.rotation = Quaternion.Euler(MainCam.transform.rotation.x, -90, 0);
        if (MainCam.transform.rotation.y > 30)
            MainCam.transform.rotation = Quaternion.Euler(MainCam.transform.rotation.x, 30, 0);
        //print(MainCam.ScreenToWorldPoint(Input.mousePosition));
    }

    // float x = 0, y = 0; // 2번
    private void CameraRotate(Vector3 mousePos)
    {

        /* 1. 모서리로 가면 이동하는 방법
            if (mousePos.y > Screen.height - _camBorderThickness)
                MainCam.transform.rotation = Quaternion.Lerp(MainCam.transform.rotation, Quaternion.Euler(20f, -45f, 0f), Time.deltaTime * _camRotateSpeed);
            else if (mousePos.y < _camBorderThickness)
                MainCam.transform.rotation = Quaternion.Lerp(MainCam.transform.rotation, Quaternion.Euler(65f, -45f, 0f), Time.deltaTime * _camRotateSpeed);

            if (mousePos.x > Screen.width - _camBorderThickness)
                MainCam.transform.rotation = Quaternion.Lerp(MainCam.transform.rotation, Quaternion.Euler(45f, -20f, 0f), Time.deltaTime * _camRotateSpeed);
            else if (mousePos.x < _camBorderThickness)
                MainCam.transform.rotation = Quaternion.Lerp(MainCam.transform.rotation, Quaternion.Euler(45f, -65f, 0f), Time.deltaTime * _camRotateSpeed);
        */

        /* 2. 마우스를 돌리면 이동하는 방법
            x += Input.GetAxis("Mouse Y") * _camRotateSpeed;
            y += Input.GetAxis("Mouse X") * _camRotateSpeed;
            x = Mathf.Clamp(x, -90, 0);
            y = Mathf.Clamp(y, -90, 0);
            transform.localEulerAngles = new Vector3(-x, y, 0);
        */

        // 3.벽에 닿은 곳으로 이동하는 방법
        // Pos = 13 10 -5
        // Rot = 45 - 45 0
        /*Ray ray = MainCam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {

            //MainCam.transform.LookAt(hit.point);
            Vector3 direction = hit.point - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 90 * Time.deltaTime * _camRotateSpeed);
        }*/

        

        MainCam.transform.LookAt(MainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mousePos.z + 100)));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(MainCam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 50)));
    }
}