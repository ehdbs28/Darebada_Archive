using JetBrains.Annotations;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fronts : MonoBehaviour, IsClickObj
{
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
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, Vector3.Distance(Camera.main.transform.position, transform.position), backLayerMask);
            //transform.localPosition = transform.position -new Vector3(pos.x, pos.y);
            if (isHorizontal)
            {
                theta = Mathf.Atan2(hit.point.y - distance.y, hit.point.x - distance.x) * Mathf.Rad2Deg;
                if (theta < min || theta > max) return;
                transform.localPosition = new Vector3(Mathf.Clamp(hit.point.x - distance.x, 0.1f, x), Mathf.Clamp(hit.point.y - distance.y, 0.1f, y)); ; ;
            }
            else
            {
                theta = Mathf.Atan2(hit.point.y - distance.y, hit.point.z - distance.z) * Mathf.Rad2Deg;
                if (theta < min || theta > max) return;
                transform.localPosition = new Vector3(Mathf.Clamp(hit.point.z - distance.z, 0.1f, z), Mathf.Clamp(hit.point.y - distance.y, 0.1f, y)); ; ;
            }
            float deg = -(45.0f - theta) * 2.0f;
            transform.localRotation = Quaternion.Euler(0, 0, deg);

        }
        if (transform.localPosition.y >= y || transform.localPosition.x >= x) isOver = true;



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
    }
}
