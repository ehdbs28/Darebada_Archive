using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBoatUpgradeContent : UIUpgradeContent
{
    public UIBoatUpgradeContent(VisualElement root, int index) : base(root, index)
    {
        List<VisualElement> updradeItems;
        updradeItems = root.Q<ScrollView>("boat-items").Query(className: "boat-item").ToList();

        foreach(var item in updradeItems){
            _buyContent.Add(new UIBoatBuyContent(item));
        }
    }
}
