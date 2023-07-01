using Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField]float handlingTime;
    [SerializeField]float limitTime;
    [SerializeField] LayerMask _layerMask;
    [SerializeField] private Vector3 _onTouchPos;
    [SerializeField] private Vector3 _onMovedPos;
    [SerializeField] private Vector3 _dir;

    private void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     handlingTime = 0;
        //     _onTouchPos = GameManager.Instance.GetManager<InputManager>().MousePosition;

        // }
        // else if (Input.GetMouseButton(0))
        // {
        // if(GameManager.Instance.GetManager<AquariumManager>().state == AquariumManager.STATE.MOVE)
        // {
        //     handlingTime += Time.deltaTime;
        //     _onMovedPos = GameManager.Instance.GetManager<InputManager>().MousePosition;
        //     Vector3 temp = _onTouchPos - _onMovedPos;
        //     _dir = new Vector3(temp.x, 0, temp.y).normalized / 2;
        //     mainCam.transform.position += (_dir);
        //     _onTouchPos = _onMovedPos;
        // }
        // }
        // else if (Input.GetMouseButtonUp(0))
        // {
        //     OpenUpgradePanel();
        // }
    }
    
    public void OpenUpgradePanel()
    {
        if (GameManager.Instance.GetManager<AquariumManager>().state == AquariumManager.STATE.MOVE)
        {
            RaycastHit hit;
            Ray ray = Define.MainCam.ScreenPointToRay(GameManager.Instance.GetManager<InputManager>().MousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
            {
                if (hit.collider.GetComponent<Fishbowl>())
                {
                    //OpenPanel(fishbowlUpgradePanel, hit.collider.gameObject);
                }
                else if (hit.collider.GetComponent<SnackShop>())
                {
                    //OpenPanel(shopUpgradePanel, hit.collider.gameObject);
                }
            }
        }
    }

    public void OpenPanel(StatePanel statePanel, GameObject selectedObject)
    {
        statePanel.upgradeObj = selectedObject;
        statePanel.gameObject.SetActive(true);
    }
}
