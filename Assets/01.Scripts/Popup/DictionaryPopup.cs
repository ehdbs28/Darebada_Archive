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
        _exitBtn.RegisterCallback<ClickEvent>(e => {
            RemoveRoot();
        });

        foreach(VisualElement fishes in _fishElements)
        {
            fishes.RegisterCallback<ClickEvent>(e =>
            {
                //���߿� �߰��Ǹ� �־����.
                //fishes.AddToClassList("");
                Debug.Log("Dictionary");
            });
        }
    }
    
    public override void RemoveEvent()
    {
    }

    protected override void FindElement(VisualElement root)
    {
        _exitBtn = root.Q<VisualElement>("exit-btn");
        _fishElements = root.Query<VisualElement>(className: "fish-element").ToList();
    }
}
