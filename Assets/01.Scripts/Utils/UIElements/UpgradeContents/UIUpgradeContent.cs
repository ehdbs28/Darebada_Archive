using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIUpgradeContent
{
    private int _index;
    public int Index => _index;

    private Label _goldLabel;

    protected List<UIBuyContent> _buyContent = new List<UIBuyContent>();

    public UIUpgradeContent(VisualElement root, int index){
        _index = index;
        _goldLabel = root.Q<Label>("gold-text");

        AddEvent();
    }

    private void AddEvent(){
        
    }
}
