using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class RotateCam : VCam
{
    private CinemachineFreeLook _freelookCam;

    private void Awake()
    {
        _freelookCam = GetComponent<CinemachineFreeLook>();
    }

    public void SetCamValue(Transform target)
    {
        Vector3 camPos = Define.MainCam.transform.position;
        Quaternion camRot = Define.MainCam.transform.rotation;
        
        _freelookCam.Follow = target;
        _freelookCam.LookAt = target;

        // �� �Լ��� �־��� ���� ������ ���� ����� ��ġ�� �̵�������
        
        _freelookCam.ForceCameraPosition(camPos, camRot);
    }

    public override void UpdateCam()
    {
        if (!GameManager.Instance.GetManager<CameraManager>()._isCanRotate)
        {
            _freelookCam.Follow = null;
            _freelookCam.LookAt = null;
        }
    }

    public void SetCamPos(float height, float radius)
    {
        for(int i = 0; i < 3; i++)
        {
            _freelookCam.m_Orbits[i].m_Height = height;
            _freelookCam.m_Orbits[i].m_Radius = radius;
        }
    }
}
