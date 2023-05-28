using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBuyElement
{
    protected VisualElement _buyButton;

    protected float _price;
    public float Price => _price;

    public UIBuyElement(VisualElement elementRoot){
        _buyButton = elementRoot.Q<VisualElement>("buy-btn");

        AddEvent();
    }

    protected virtual void AddEvent(){
        _buyButton.RegisterCallback<ClickEvent>(e => {
            Debug.Log("구매");
        });
    }
}
