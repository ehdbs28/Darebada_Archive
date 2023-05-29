using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBuyContent : UIPopupContent
{
    public UIBuyContent(VisualElement root, int index) : base(root, index)
    {
        List<VisualElement> buyItems;
        buyItems = root.Q<ScrollView>("buy-items").Query(className: "buy-item").ToList();

        foreach(var item in buyItems){
            _buyContent.Add(new UIBuyElement(item));
        }

        AddEvent();
    }
}
