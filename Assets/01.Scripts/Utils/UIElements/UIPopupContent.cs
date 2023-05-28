using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIPopupContent
{
    private int _index;
    public int Index => _index;

    private Label _goldLabel;

    protected List<UIBuyElement> _buyContent = new List<UIBuyElement>();

    public UIPopupContent(VisualElement root, int index){
        _index = index;
        _goldLabel = root.Q<Label>("gold-text");
    }

    protected virtual void AddEvent(){
        // connect gold event
    }
}
