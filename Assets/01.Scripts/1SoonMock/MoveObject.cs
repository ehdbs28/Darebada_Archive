using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class MoveObject : MonoBehaviour, IsClickObj
{
    public Vector3 distance;
    public LayerMask layerMask;
    public bool isHold;
    public float x, y, z;
    public bool holdX, holdY, holdZ;
    public void OnClick()
    {
        isHold = true;
        distance = Vector3.zero;
        Debug.LogError(distance);
    }

    public void OnDrag()
    {

    }

    public void OnDragEnd()
    {
        isHold = false;
        distance = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(isHold)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (distance == Vector3.zero) { distance = transform.position - hit.point; }
                Debug.Log(hit.point + distance);
                transform.position = hit.point +distance;
                if (holdX) transform.position = new Vector3(x,transform.position.y,transform.position.z);
                if(holdY) transform.position = new Vector3(transform.position.x, y, transform.position.z);
                if(holdZ) transform.position = new Vector3(transform.position.x, transform.position.y, z);
            }
            else
            {
                isHold = false;
            }
        }

    }
}
