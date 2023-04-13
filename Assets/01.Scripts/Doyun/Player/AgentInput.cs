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
            // 나중에는 카메라 캐싱해서 사용해야 함
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isHit = Physics.Raycast(ray, out hit, Camera.main.farClipPlane, LayerMask.GetMask("Ground"));

            if (isHit)
            {
                OnMouseClickEvent?.Invoke(hit.point);
            }
        }
    }
}
