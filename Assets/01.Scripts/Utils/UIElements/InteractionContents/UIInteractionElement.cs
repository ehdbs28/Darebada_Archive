using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIInteractionElement
{
    protected VisualElement _root;
    protected VisualElement _interactionBtn;

    public UIInteractionElement(VisualElement root)
    {
        _root = root;
    }

    protected virtual void FindElement(){
        _interactionBtn = _root.Q<VisualElement>(className: "interaction-btn");
    }

    protected abstract void AddEvent();
}
