using System.Collections.Generic;
using UnityEngine.UIElements;

public class UIDecoContent
{
    private VisualElement _root;
    private Fishbowl _fishbowl;

    private List<UIDecoUnit> _decoUnits = new List<UIDecoUnit>();
    
    public UIDecoContent(VisualElement root, Fishbowl fishbowl)
    {
        _root = root;
        _fishbowl = fishbowl;
        
        FindElement();
    }

    private void FindElement()
    {
        var unitParent = _root.Q("units");
        for (int i = 0; i < unitParent.childCount; i++)
        {
            var unitElem = unitParent.ElementAt(i);
            if(unitElem.name == "deco-unit-nonuse")
                continue;
            _decoUnits.Add(new UIDecoUnit(unitElem, _fishbowl, i));
        }
    }

    private void AddEvent()
    {
        
    }
}
