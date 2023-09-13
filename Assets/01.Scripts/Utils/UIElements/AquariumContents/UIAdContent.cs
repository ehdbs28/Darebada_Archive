using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIAdContent : UIPopupContent
{
    private List<UIAdUnit> _units = new List<UIAdUnit>();
    
    public UIAdContent(VisualElement root, int index) : base(root, index)
    {
    }

    protected override void FindElement()
    {
        base.FindElement();

        var adUnitParent = _root.Q("ad-items");
        var price = 10;
        var value = 10;

        for (int i = 0; i < adUnitParent.childCount; i++)
        {
            var unit = adUnitParent.ElementAt(i);
            _units.Add(new UIAdUnit(unit, _goldLabel, price, value));
        }
    }
}
