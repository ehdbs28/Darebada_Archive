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
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            CameraRotate(Input.mousePosition);

        
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

        /*실패
        Quaternion targetRot = Quaternion.LookRotation(targetPos - MainCam.transform.position);

        MainCam.transform.rotation = Quaternion.Lerp(MainCam.transform.rotation, targetRot, _camRotateSpeed * 0.1f);
        */
        //MainCam.transform.LookAt(MainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mousePos.z + 100)));


        Vector3 targetPos = MainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mousePos.z + 100));
        Vector3 dir = targetPos - MainCam.transform.position;

        Vector3 rot = transform.eulerAngles;


        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(MainCam.transform.rotation, targetRot, 90 * Time.deltaTime * _camRotateSpeed);

        // 제한 걸기
        Vector3 euler = MainCam.transform.eulerAngles;

        //if (euler.x < 5)
        //    euler.x = 5f;
        //if (euler.x > 70)
        //    euler.x = 70f;

        /*if (euler.y > 5f)
            euler.y = 5f;*/

        euler.y = Mathf.Clamp(euler.y, 270f, 359f);
        //if (euler.y < 270f)
        //    euler.y = 270f;


        MainCam.transform.rotation = Quaternion.Euler(euler);
    }
}