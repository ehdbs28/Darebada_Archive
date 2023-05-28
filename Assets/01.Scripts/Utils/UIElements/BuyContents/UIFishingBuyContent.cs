using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIFishingBuyContent : UIBuyContent
{
    private VisualElement _valueElement;

    private float _value;
    public float Vlaue => _value;

    public UIFishingBuyContent(VisualElement elementRoot) : base(elementRoot)
    {
        
    }

    protected override void AddEvent()
    {
        
    }
}
