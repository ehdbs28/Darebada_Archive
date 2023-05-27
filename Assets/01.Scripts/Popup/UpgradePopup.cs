using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradePopup : UIPopup
{
    private VisualElement _boatBtn;
    private VisualElement _fishingBtn;

    protected override void AddEvent(VisualElement root)
    {

    }

    protected override void FindElement(VisualElement root)
    {
        _boatBtn = root.Q<VisualElement>("boat-btn");
        _fishingBtn = root.Q<VisualElement>("fishing-btn");
    }
}
