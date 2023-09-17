using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public struct UIFishDeleteUnit
{
    public VisualElement root;
    public FishDataUnit dataUnit;
    public int idx;
}

public class UIFishDeleteContent
{
    private VisualElement _root;
    private VisualElement _unitParent;
    private VisualElement _deleteBtn;

    private Fishbowl _fishbowl;

    private List<UIFishDeleteUnit> _units = new List<UIFishDeleteUnit>();
    private List<int> _deleteUnits = new List<int>();

    public UIFishDeleteContent(VisualElement root, Fishbowl fishbowl)
    {
        _root = root;
        _fishbowl = fishbowl;

        FindElement();
        AddEvent();
    }

    private void FindElement()
    {
        _deleteBtn = _root.Q("removefish-btn");

        _unitParent = _root.Q("units");
        for (int i = 0; i < _unitParent.childCount; i++)
        {
            var unit = _unitParent.ElementAt(i);
            var imageBox = unit.Q("fish-image");
            FishDataUnit unitData = null;

            if (imageBox != null)
            {
                if (i < _fishbowl.fishs.Count)
                {
                    unitData = _fishbowl.fishs[i].UnitData;
                    unit.Q("fish-image").style.backgroundImage = new StyleBackground(unitData.Visual.Profile);
                }
                else
                {
                    unit.Q("fish-image").style.backgroundImage = null;
                }

                _units.Add(new UIFishDeleteUnit{ root = unit, dataUnit = unitData, idx = i });
            }
        }
    }

    private void AddEvent()
    {
        foreach (var unit in _units)
        {
            unit.root.RegisterCallback<ClickEvent>(e =>
            {
                var checkElem = unit.root.Q("check");
                if (checkElem.style.opacity == 0)
                {
                    checkElem.style.opacity = 1;
                    _deleteUnits.Add(unit.idx);
                }
                else
                {
                    checkElem.style.opacity = 0;
                    _deleteUnits.Remove(unit.idx);
                }
            });
        }
        
        _deleteBtn.RegisterCallback<ClickEvent>(e =>
        {
            foreach (var idx in _deleteUnits)
            {
                _fishbowl.RemoveFish(idx);
                _units[idx].root.Q("check").style.opacity = 0;
                _units[idx].root.style.backgroundImage = null;
            }
            _deleteUnits.Clear();
        });
    }
}
