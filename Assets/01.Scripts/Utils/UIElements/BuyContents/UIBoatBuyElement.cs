using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBoatBuyElement : UIBuyElement
{
    private BoatBuyState _buyState;
    public BoatBuyState BuyState => _buyState;

    public UIBoatBuyElement(VisualElement elementRoot) : base(elementRoot)
    {
        _buyState = BoatBuyState.SALE;
    }

    protected override void AddEvent()
    {
        _buyButton.RegisterCallback<ClickEvent>(e => {
            switch(_buyState){
                case BoatBuyState.SALE:
                    Debug.Log("구매");
                    break;
                case BoatBuyState.BOUGHT:
                    Debug.Log("장착");
                    break;
            }
        });
    }
}
