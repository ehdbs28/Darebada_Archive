using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiButtonObject : MonoBehaviour, IButtonObject
{
    public UnityEvent OnTouchEvent = null; 

    public void OnTouch()
    {
        OnTouchEvent?.Invoke();
    }
    
}
