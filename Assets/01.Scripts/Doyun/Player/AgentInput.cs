using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentInput : MonoBehaviour
{
    public event Action<Vector3> OnMouseClickEvent = null;

    private void Update()
    {
        MouseClick();
    }

    private void MouseClick()
    {

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = CameraManager.Instance.MainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isHit = Physics.Raycast(ray, out hit, CameraManager.Instance.MainCam.farClipPlane, LayerMask.GetMask("Ground"));

            if (isHit)
            {
                Debug.Log(hit.transform.name);
                OnMouseClickEvent?.Invoke(hit.point);
            }
        }
    }
}
