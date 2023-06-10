using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiButtonObject : MonoBehaviour, IButtonObject
{
    [SerializeField] PopupType popType;
    public void OnTouch()
    {
        GameManager.Instance.GetManager<UIManager>().ShowPanel(popType);
    }
    
}
