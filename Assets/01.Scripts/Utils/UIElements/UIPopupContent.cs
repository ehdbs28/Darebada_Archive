using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIPopupContent
{
    protected VisualElement _root;

    protected int _index;
    public int Index => _index;

    protected Label _goldLabel;

    protected List<UIBuyElement> _buyContent = new List<UIBuyElement>();

    public UIPopupContent(VisualElement root, int index){
        _root = root;
        _index = index;
        FindElement();
        AddEvent();
    }

    protected virtual void FindElement(){
        _goldLabel = _root.Q<Label>("gold-text");
    }

    protected virtual void AddEvent(){
        // connect gold event
    }
}
