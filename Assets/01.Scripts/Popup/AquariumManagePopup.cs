using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AquariumManagePopup : UIPopup
{
    private VisualElement _exitBtn;

    protected override void AddEvent(VisualElement root)
    {
        _exitBtn.RegisterCallback<ClickEvent>(e => {
            RemoveRoot();
        });
    }

    public override void RemoveEvent()
    {
    }

    protected override void FindElement(VisualElement root)
    {
        _exitBtn = root.Q<VisualElement>("exit-btn");
    }
}
