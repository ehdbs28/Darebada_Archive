using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CastingPopup : UIPopup
{
    private VisualElement _valueBar;
    
    private Vector3 _valueScale;
    private float _heigthSetValue = 0.5f;

    protected override void AddEvent(VisualElement root)
    {
        _valueScale = _valueBar.transform.scale;
        _valueBar.transform.scale = new Vector3(_valueScale.x, Mathf.Sin(Time.deltaTime * 0.5f) * _heigthSetValue + _heigthSetValue, _valueScale.z);
    }

    protected override void FindElement(VisualElement root)
    {
        _valueBar = root.Q<VisualElement>("value");
    }
}  
