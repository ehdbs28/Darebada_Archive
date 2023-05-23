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
    Camera mainCam;
    [SerializeField] LayerMask _layerMask;
    [SerializeField] private Vector3 _onTouchPos;
    [SerializeField] private Vector3 _onMovedPos;
    [SerializeField] private Vector3 _dir;
    EventSystem sysl;
    private void Awake()
    {
        mainCam = Define.MainCam;
    }
    private void Update()
    {
        
        if(Input.GetMouseButtonDown(0) && !sysl.IsPointerOverGameObject())
        {

            if (Input.GetMouseButtonDown(0))
            {
                handlingTime = 0;
                _onTouchPos = GameManager.Instance.GetManager<InputManager>().MousePosition;

            }
            else if (Input.GetMouseButton(0))
            {
                handlingTime += Time.deltaTime;
                _onMovedPos = GameManager.Instance.GetManager<InputManager>().MousePosition;
                Vector3 temp = _onTouchPos - _onMovedPos;
                _dir = new Vector3(temp.x, 0, temp.y).normalized / 2;
                mainCam.transform.position += (_dir);
                _onTouchPos = _onMovedPos;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (handlingTime <= limitTime)
                {

                    if (!fishbowlUpgradePanel.gameObject.activeSelf && !shopUpgradePanel.gameObject.activeSelf && !addPanel.activeSelf)
                    {
                        RaycastHit hit;
                        Ray ray = Define.MainCam.ScreenPointToRay(GameManager.Instance.GetManager<InputManager>().MousePosition);
                        Debug.Log(GameManager.Instance.GetManager<AquariumManager>().state);

                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask) && GameManager.Instance.GetManager<AquariumManager>().state == AquariumManager.STATE.BUILD)
                        {

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
}
