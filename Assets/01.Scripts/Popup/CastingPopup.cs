using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CastingPopup : UIPopup
{
    private VisualElement _valueBar;
    
    public override void AddEvent()
    {
    }
    
    public override void RemoveEvent()
    {
    }

    public override void FindElement()
    {
        _valueBar = _root.Q<VisualElement>("value");
    }

    public void SetValue(float value){
        _valueBar.style.scale = new StyleScale(new Scale(new Vector3(1, value)));
    }
}  
