using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopPopup : UIPopup
{
    private VisualElement _sellBtn;
    private VisualElement _buyBtn;

    protected override void AddEvent(VisualElement root)
    {

    }

    protected override void FindElement(VisualElement root)
    {
        _sellBtn = root.Q<VisualElement>("sell-btn");
        _buyBtn = root.Q<VisualElement>("buy-btn");
    }
}
