using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneObjectManager : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;

    public void AddEvent()
    {
        GameManager.Instance.GetManager<InputManager>().OnMouseClickEvent += MouseClickHandle;    
    }

    public void RemoveEvent(){
        GameManager.Instance.GetManager<InputManager>().OnMouseClickEvent -= MouseClickHandle;
    }

    private void MouseClickHandle(bool value)
    {
        Ray ray = Define.MainCam.ScreenPointToRay(GameManager.Instance.GetManager<InputManager>().MousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,Mathf.Infinity, _layerMask))
        {
            hit.collider.GetComponent<UiButtonObject>().OnTouch();
        }
    }

    public void SetPopup(int type, int popupNum, bool isPopup){
        GameManager.Instance.GetManager<UIManager>().ShowPanel((PopupType)type);
        ((FacilityEntryPopup)GameManager.Instance.GetManager<UIManager>().GetPanel((PopupType)type))._currFacility = popupNum;
    }

    public void ChangeScene(int type){
        GameManager.Instance.GetManager<GameSceneManager>().ChangeScene((GameSceneType)type);
    }
}
