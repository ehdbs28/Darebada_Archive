using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UISellContent : UIPopupContent
{
    private VisualElement _sellBtn;

    private Label _nameText;
    private Label _priceText;

    private VisualElement _fishImage;

    public UISellContent(VisualElement root, int index) : base(root, index)
    {
        _sellBtn = root.Q("sell-btn");

        _nameText = root.Q<Label>("name-text");
        _priceText = root.Q<Label>("price-text");

        _fishImage = root.Q("fish-image");

        AddEvent();
    }

    protected override void AddEvent()
    {
        base.AddEvent();

        _sellBtn.RegisterCallback<ClickEvent>(e => {
            Debug.Log("판매 완료");
        });
    }
}
