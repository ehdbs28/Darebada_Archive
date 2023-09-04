using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiButtonObject : MonoBehaviour, IButtonObject
{
    [SerializeField]
    private MainSceneObjectManager _manager;

    [SerializeField]
    private PopupType type;

    [SerializeField]
    private PopupType popupNum;

    [SerializeField] 
    private GameSceneType sceneType;

    [SerializeField]
    private bool _isPopup;
    
    [SerializeField]
    private bool _isConfirm;

    public void OnTouch()
    {
        if (_isConfirm)
        {
            _manager.SetPopup(type, popupNum, sceneType, _isPopup);
        }
        else
        {
            if (_isPopup)
            {
                GameManager.Instance.GetManager<UIManager>().ShowPanel(popupNum);
            }
            else
            {
                GameManager.Instance.GetManager<GameSceneManager>().ChangeScene(sceneType);
            }
        }
    }
}
