using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBoatBuyElement : UIBuyElement
{
    private int _idx;

    private BoatBuyState _buyState;
    public BoatBuyState BuyState { get => _buyState; set => _buyState = value; }

    private VisualElement _root;
    public VisualElement Root => _root;

    private List<UIBoatBuyElement> _boatUIs;

    public UIBoatBuyElement(VisualElement elementRoot, int idx) : base(elementRoot)
    {
        _root = elementRoot;
        _buyState = BoatBuyState.SALE;
        _idx = idx;
    }

    protected override void AddEvent()
    {
        _buyButton.RegisterCallback<ClickEvent>(e => {
            switch(_buyState){
                case BoatBuyState.SALE:
                    Debug.Log($"{_idx}구매");
                    _root.AddToClassList("unlock");
                    break;
                case BoatBuyState.BOUGHT:
                    Debug.Log($"{_idx}장착");
                    for(int i = 0; i < _boatUIs.Count; i++){
                        if(i == _idx)
                            continue;

                        SelectBoat(i, false);
                    }
                    SelectBoat(_idx, true);
                    break;
            }
        });
    }

    private void ListSet(List<UIBoatBuyElement> list){
        _boatUIs = list;
    }

    private void SelectBoat(int idx, bool select){
        UIBoatBuyElement boatUI = _boatUIs[idx];
        
        if(select){
            boatUI.BuyState = BoatBuyState.EQUIP;
            boatUI.Root.AddToClassList("select");
            GameManager.Instance.GetManager<BoatManager>().SelectBoat(idx);
        }
        else{
            boatUI.BuyState = BoatBuyState.BOUGHT;
            boatUI.Root.RemoveFromClassList("select");
        }
    }
}
