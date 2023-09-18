using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneObjectManager : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;

    public void AddEvent()
    {
        GameManager.Instance.GetManager<InputManager>().OnTouchEvent += MouseClickHandle;    
    }

    public void RemoveEvent(){
        GameManager.Instance.GetManager<InputManager>().OnTouchEvent -= MouseClickHandle;
    }

    private void MouseClickHandle()
    {
        if (GameManager.Instance.GetManager<TutorialManager>().InTut)
            return;
        
        Ray ray = Define.MainCam.ScreenPointToRay(GameManager.Instance.GetManager<InputManager>().TouchPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
        {
            hit.collider.GetComponent<UiButtonObject>().OnTouch();
        }
    }

    public void SetPopup(PopupType type, PopupType popupNum, GameSceneType sceneType, bool isPopup){
        GameManager.Instance.GetManager<UIManager>().ShowPanel(type);
        ((FacilityEntryPopup)GameManager.Instance.GetManager<UIManager>().GetPanel(type)).isPopup = isPopup;
        if (isPopup)
        {
            ((FacilityEntryPopup)GameManager.Instance.GetManager<UIManager>().GetPanel(type)).popupType = popupNum;
        }
        else
        {
            ((FacilityEntryPopup)GameManager.Instance.GetManager<UIManager>().GetPanel(type)).sceneType = sceneType;
        }
    }

    public void ChangeScene(int type){
        GameManager.Instance.GetManager<GameSceneManager>().ChangeScene((GameSceneType)type);
    }
}
