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
    public StatePanel fishbowlUpgradePanel;
    public StatePanel shopUpgradePanel;
    public GameObject addPanel;
    [SerializeField]float handlingTime;
    [SerializeField]float limitTime;
    [SerializeField] LayerMask _layerMask;
    [SerializeField] private Vector3 _onTouchPos;
    [SerializeField] private Vector3 _onMovedPos;
    [SerializeField] private Vector3 _dir;
    EventSystem sysl;

    private bool _isMouseClicked = false;

    private void Update()
    {
        if (_isMouseClicked)
        {
            if(GameManager.Instance.GetManager<AquariumManager>().state == AquariumManager.STATE.MOVE)
            {

                handlingTime += Time.deltaTime;
                _onMovedPos = GameManager.Instance.GetManager<InputManager>().MousePosition;
                Vector3 temp = _onTouchPos - _onMovedPos;
                _dir = new Vector3(temp.x, 0, temp.y).normalized / 2;
                Define.MainCam.transform.position += (_dir);
                _onTouchPos = _onMovedPos;
            }
        }
    }

    public void Init(){
        GameManager.Instance.GetManager<InputManager>().OnMouseClickEvent += MouseClickHandle;
    }

    public void Exit(){
        GameManager.Instance.GetManager<InputManager>().OnMouseClickEvent -= MouseClickHandle;
    }

    private void MouseClickHandle(bool value){
        _isMouseClicked = value;

        if(_isMouseClicked){
            handlingTime = 0;
            _onTouchPos = GameManager.Instance.GetManager<InputManager>().MousePosition;
        }
        else{
            if (handlingTime <= limitTime)
            {

                if (!fishbowlUpgradePanel.gameObject.activeSelf && !shopUpgradePanel.gameObject.activeSelf && !addPanel.activeSelf && GameManager.Instance.GetManager<AquariumManager>().state == AquariumManager.STATE.MOVE)
                {
                    RaycastHit hit;
                    Ray ray = Define.MainCam.ScreenPointToRay(GameManager.Instance.GetManager<InputManager>().MousePosition);

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask) )
                    {
                    Debug.Log(GameManager.Instance.GetManager<AquariumManager>().state);

                        if (hit.collider.GetComponent<Fishbowl>())
                        {
                            fishbowlUpgradePanel.upgradeObj = hit.collider.gameObject;
                            fishbowlUpgradePanel.gameObject.SetActive(true);
                        }
                        else if (hit.collider.GetComponent<SnackShop>())
                        {
                            shopUpgradePanel.upgradeObj = hit.collider.gameObject;
                            shopUpgradePanel.gameObject.SetActive(true);
                        }
                    }

                }
            }
        }
    }
}
