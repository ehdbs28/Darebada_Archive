using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBoatUpgradeContent : UIPopupContent
{
    public UIBoatUpgradeContent(VisualElement root, int index) : base(root, index)
    {
        List<VisualElement> upgradeItems;
        upgradeItems = root.Q<ScrollView>("boat-items").Query(className: "boat-item").ToList();

        foreach(var item in upgradeItems){
            _buyContent.Add(new UIBoatBuyElement(item));
        }

        AddEvent();
    }
}
