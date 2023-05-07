using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiButtonObject : MonoBehaviour, IButtonObject
{
    [SerializeField] GameObject _upgradeUI;
    public void OnClick()
    {
        _upgradeUI.SetActive(true);
    }
    
}
