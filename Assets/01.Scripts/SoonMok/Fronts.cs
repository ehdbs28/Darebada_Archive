using JetBrains.Annotations;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fronts : MonoBehaviour, IsClickObj
{
    [SerializeField] bool isOver =false;
    [SerializeField]bool isHolding = false;
    public float max;
    public float min;
    public float theta;
    public float angle;
    public float x;
    public float y;
    public Vector3 distance;
    private void Awake()
    {
        //x¶û y °ª Á¤ÇØÁà¾ßÇÔ'
    }
    private void Update()
    {
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, 0.1f, x), Mathf.Clamp(transform.localPosition.y, 0.1f, y), 0);
    }
    void LateUpdate()
    {
        if(isOver)
        {
            isHolding = false;
            GetComponent<ZIpperResult>().ShowResult();
            this.enabled = false;
        }
        if (isHolding)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
            theta = Mathf.Atan2(hit.point.y, hit.point.x) * Mathf.Rad2Deg;
            if (theta < min || theta > max) return;
                if (distance == Vector3.zero) { distance = transform.parent.position - hit.point; }
                transform.localPosition = hit.point + distance;

            }
            transform.localPosition = new Vector3(Mathf.Clamp(hit.point.x, 0.1f, x), Mathf.Clamp(hit.point.y, 0.1f, y), 0);
            float deg = -(45.0f - theta) * 2.0f;
            //transform.eulerAngles = new Vector3(0, 0, deg);
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
        if(!isOver)
            Hold(true);
        distance = Vector3.zero;
    }

    public void OnDrag()
    {
        
    }

    public void OnDragEnd()
    {
        if(!isOver)
            Hold(false);
    }
}
