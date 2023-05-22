using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public StatePanel fishbowlUpgradePanel;
    public StatePanel shopUpgradePanel;
    public GameObject addPanel;
    [SerializeField]float handlingTime;
    [SerializeField]float limitTime;
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
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            handlingTime = 0;
        }
        else if (Input.GetMouseButton(0))
        {
            handlingTime += Time.deltaTime;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(handlingTime <= limitTime)
            {
                if(!fishbowlUpgradePanel.gameObject.activeSelf && !shopUpgradePanel.gameObject.activeSelf && !addPanel.activeSelf)
                {
                    RaycastHit hit;
                    Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
                    Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask);
                    
                    //if (hit.collider.gameObject.GetComponent<Facility>() && AquariumManager.Instance.state != AquariumManager.STATE.BUILD)
                    //{
                    //    GameObject hitObj = hit.collider.gameObject.GetComponent<Facility>().OnTouched().gameObject;
                    //    if(hitObj.GetComponent<Fishbowl>())
                    //    {
                    //        fishbowlUpgradePanel.upgradeObj = hitObj;
                    //        fishbowlUpgradePanel.gameObject.SetActive(true);
                    //    }
                    //    else if(hitObj.GetComponent<SnackShop>())
                    //    {
                    //        shopUpgradePanel.upgradeObj = hitObj;
                    //        shopUpgradePanel.gameObject.SetActive(true);
                    //    }
                    //}
                }
            }
        }
    }
}
