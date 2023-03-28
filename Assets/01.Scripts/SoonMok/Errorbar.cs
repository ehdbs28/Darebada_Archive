using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Errorbar : MonoBehaviour, IsClickObj
{
    [SerializeField]private bool isHold = false;
    public Vector3 startPoint;
    public LayerMask layerMask;
    public Vector3 distance ;
    public void OnClick()
    {
        SetHold(true);
        startPoint = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector3.zero;
    }

    public void OnDrag()
    {
        
    }

    public void OnDragEnd()
    {
        SetHold(false);
    }

    public void SetHold(bool value)
    {
        isHold = value;
    }
    void Update()
    {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);
        if(isHold)
        {

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (distance == Vector3.zero) { distance = transform.position - hit.point ; }
                Debug.Log(hit.point + distance);
                transform.parent.position = hit.point + distance;
            }

        }
    }
}
