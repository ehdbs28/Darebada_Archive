using JetBrains.Annotations;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fronts : MonoBehaviour, IsClickObj
{
    public enum BackPivot
    {
        x,y,z
    }
    public BackPivot back;
    [SerializeField] bool isOver = false;
    [SerializeField] bool isHolding = false;
    public bool isHorizontal;
    public float max;
    public float min;
    public float theta;
    public float angle;
    public float x;
    public float y;
    public float z;
    public Vector3 distance;
    public LayerMask backLayerMask;
    private void Awake()
    {
        //x�� y �� ���������'
    }
    public Vector3 hitPoint;
    private void Update()
    {
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, 0.1f, x), Mathf.Clamp(transform.localPosition.y, 0.1f, y), Mathf.Clamp(transform.localPosition.z, 0f, z));
    }
    void LateUpdate()
    {
        if (isOver)
        {
            isHolding = false;
            GetComponent<ZIpperResult>().ShowResult();
            this.enabled = false;
        }
        if (isHolding)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (isHolding)
            {
            RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, backLayerMask))
                {
                    if (distance == Vector3.zero) { distance = transform.position - hit.point; }
                hitPoint = hit.point - distance;   

                    switch (back)
                    {
                        case BackPivot.x:
                            theta = Mathf.Atan2(hit.point.y , hit.point.x )* Mathf.Rad2Deg;
                            transform.localPosition = new Vector3(Mathf.Clamp(hit.point.x, 0.1f, x), Mathf.Clamp(hit.point.y, 0.1f, y), Mathf.Clamp(hit.point.z, 0f, z));
                             if (theta < min || theta > max) return;
                            break;
                        case BackPivot.y:
                            break;
                        case BackPivot.z:

                            theta = Mathf.Atan2(hit.point.y - distance.y, hit.point.z - distance.z) * Mathf.Rad2Deg *-1;
                            transform.localPosition = new Vector3(Mathf.Clamp(-hit.point.z, -x, x), Mathf.Clamp(hit.point.y, 0.1f, y), Mathf.Clamp(hit.point.z, 0f, z));
                             if (theta  < -180 || theta > -90) return;
                            
                            break;

                    }

                    
                    float deg = -(45.0f - theta) * 2.0f;
                    transform.localRotation = Quaternion.Euler(0, 0, deg);
                }
            }
            //transform.localPosition = transform.position -new Vector3(pos.x, pos.y);

        }



    }

    public void Hold(bool value)
    {
        isHolding = value;
    }

    public void OnClick()
    {
        if (!isOver)
            Hold(true);
        distance = transform.position;
    }

    public void OnDrag()
    {

    }

    public void OnDragEnd()
    {
        if (!isOver)
            Hold(false);
        distance = Vector3.zero;
    }
}
