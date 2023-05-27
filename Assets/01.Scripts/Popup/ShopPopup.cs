using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopPopup : UIPopup
{
    private VisualElement _sellBtn;
    private VisualElement _buyBtn;

    private VisualElement _buyTap;
    private VisualElement _sellTap;

    protected override void AddEvent(VisualElement root)
    {
        _sellBtn.RegisterCallback<ClickEvent>(e =>
        {
            _sellTap.BringToFront();
        });

        _buyBtn.RegisterCallback<ClickEvent>(e =>
        {
            _buyTap.BringToFront();
        });
    }

    protected override void FindElement(VisualElement root)
    {
        _sellBtn = root.Q<VisualElement>("sell-btn");
        _buyBtn = root.Q<VisualElement>("buy-btn");

        _buyTap = root.Q<VisualElement>("sell-contant");
        _sellTap = root.Q<VisualElement>("buy-content");
    }
}
