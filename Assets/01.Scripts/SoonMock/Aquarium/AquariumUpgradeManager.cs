using Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class AquariumUpgradeManager : MonoBehaviour
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
    }

    public void OpenPanel(StatePanel statePanel, GameObject selectedObject)
    {
        statePanel.upgradeObj = selectedObject;
        statePanel.gameObject.SetActive(true);
    }
}
