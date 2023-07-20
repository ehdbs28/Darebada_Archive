using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIBuyElement
{
    protected VisualElement _root;
    protected VisualElement _buyBtn;

    protected virtual void FindElement(){
        _buyBtn = _root.Q<VisualElement>("buy-btn");
    }

    protected abstract void AddEvent();
}
