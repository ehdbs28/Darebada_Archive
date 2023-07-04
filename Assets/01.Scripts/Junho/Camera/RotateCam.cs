using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCam : VCam
{
    private CinemachineFreeLook _freelookCam;
    private Vector3 _resetPos;

    private void Awake()
    {
        _freelookCam = GetComponent<CinemachineFreeLook>();
    }

    public void SetCamValue(Transform target)
    {
        _freelookCam.Follow = target;
        _freelookCam.LookAt = target;

        _freelookCam.ForceCameraPosition(_resetPos, Quaternion.Euler(Vector3.zero));
    }

    public override void UpdateCam()
    {
    }
}
