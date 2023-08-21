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
    private int type;

    [SerializeField]
    private int popupNum;

    [SerializeField]
    private bool _isPopup;

    public void OnTouch()
    {
        _manager.SetPopup(type, popupNum, _isPopup);
    }
}
