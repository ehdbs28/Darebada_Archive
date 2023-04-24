using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public GameObject fishbowlUpgradePanel;
    public GameObject shopUpgradePanel;
    float handlingTime;
    float limitTime;
    Camera mainCam;
    [SerializeField] LayerMask _layerMask;
    [SerializeField] private Vector3 _onTouchPos;
    [SerializeField] private Vector3 _onMovedPos;

    private void Awake()
    {
        if(mainCam!=null)
        {
            Destroy(this);
        }else
            mainCam = Camera.main;
    }
    private void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            handlingTime = 0;
        }
        else if (Input.GetMouseButton(0))
        {
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(handlingTime <= limitTime)
            {
                RaycastHit hit;
                Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask);
                GameObject hitObj = hit.collider.gameObject.GetComponent<Facility>().OnTouched().gameObject;
                if (hitObj)
                {

                }
            }
        }
    }
}
