using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIFishingUpgradeContent : UIPopupContent
{
    public UIFishingUpgradeContent(VisualElement root, int index) : base(root, index)
    {
        List<VisualElement> upgradeItems;
        upgradeItems = root.Q("upgrade-items").Query(className: "fishing-item").ToList();

        for(int i = 0; i < upgradeItems.Count; i++){
            _buyContent.Add(new UIFishingBuyElement(upgradeItems[i], i));
        }

        AddEvent();
    }
}
