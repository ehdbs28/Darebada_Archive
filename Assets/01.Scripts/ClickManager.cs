using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public Ray mouseRay;
    public Camera mainCam ;
    public LayerMask layerMask;
    private void Awake()
    {
        mainCam= Camera.main;
    }
    void Update()
    {
        mouseRay = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(mouseRay, out hit, mainCam.farClipPlane, layerMask))
        {
            IsClickObj obj = hit.collider.GetComponent<IsClickObj>();
            if (obj!=null)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    Debug.Log(hit.point);
                    Debug.Log(mainCam.ScreenPointToRay(Input.mousePosition));
                    obj.OnClick();
                }
                else if(Input.GetMouseButton(0)) 
                {
                    obj.OnDrag();
                }
                else if(Input.GetMouseButtonUp(0))
                {
                    obj.OnDragEnd();
                }
            }
            transform.position = hit.point;
        }
    }
}
