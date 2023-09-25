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

    protected List<UIInteractionElement> _buyContent = new List<UIInteractionElement>();

    public UIPopupContent(VisualElement root, int index){
        _root = root;
        _index = index;
        FindElement();
        AddEvent();
        OnGoldChanged((GameManager.Instance.GetManager<DataManager>().GetData(DataType.GameData) as GameData).HoldingGold);
    }

    protected virtual void FindElement(){
        _goldLabel = _root.Q<Label>("gold-text");
    }

    protected virtual void AddEvent()
    {
        GameManager.Instance.GetManager<MoneyManager>().OnGoldChange += OnGoldChanged;
    }

    private void OnGoldChanged(int gold)
    {
        _goldLabel.text = $"{gold:N}";
    }
}
