using System.Collections.Generic;
using UnityEngine.UIElements;

public class UICleanContent : UIPopupContent
{
    private List<UICleanUnit> _cleanUnits = new List<UICleanUnit>();
    
    public UICleanContent(VisualElement root, int index) : base(root, index)
    {
    }

    protected override void FindElement()
    {
        base.FindElement();

        var cleanItemParent = _root.Q("clean-items");
        var price = 250;
        var value = 10;
        
        for (int i = 0; i < cleanItemParent.childCount; i++)
        {
            _cleanUnits.Add(new UICleanUnit(cleanItemParent.ElementAt(i), price, value));
            price *= 2;
            value *= 2;
        }
    }
}
