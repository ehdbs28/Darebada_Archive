using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DictionaryPopup : UIPopup
{
    private VisualElement _exitBtn;
    private List<VisualElement> _fishElements = new List<VisualElement>();

    protected override void AddEvent(VisualElement root)
    {
    }

    protected override void FindElement(VisualElement root)
    {
        _exitBtn = root.Q<VisualElement>("exit-btn");
        _fishElements = root.Query<VisualElement>(className: "fish-element").ToList();
    }
}
