using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIFishingUpgradeContent : UIUpgradeContent
{
    public UIFishingUpgradeContent(VisualElement root, int index) : base(root, index)
    {
        List<VisualElement> updradeItems;
        updradeItems = root.Q("upgrade-items").Query(className: "fishing-item").ToList();

        foreach(var item in updradeItems){
            _buyContent.Add(new UIFishingBuyContent(item));
        }
    }
}
