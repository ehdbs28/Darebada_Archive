using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CastingPopup : UIPopup
{
    private VisualElement _valueBar;

    protected override void AddEvent(VisualElement root)
    {
    }

    protected override void FindElement(VisualElement root)
    {
        _valueBar = root.Q<VisualElement>("value");
    }
}  
