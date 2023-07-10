using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIFishingBuyElement : UIBuyElement
{
    private VisualElement _valueElement;

    private float _value;
    public float Vlaue => _value;

    int _idx = 0;

    public UIFishingBuyElement(VisualElement elementRoot, int idx) : base(elementRoot)
    {
        _idx = idx;
    }

    protected override void AddEvent()
    {
        _buyButton.RegisterCallback<ClickEvent>(e => {
            if(_idx == 0){
                Debug.Log("0");
            }
            else if(_idx == 1){
                Debug.Log("1");
            }
            else if(_idx == 2){
                Debug.Log("2");
            }
        });
    }
}
