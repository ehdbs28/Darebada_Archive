using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBoatDurability
{
    private Gradient _durabilityGradient;
    
    private VisualElement _circle;
    private VisualElement _boatImage;

    private BoatActionData _boatData;

    public UIBoatDurability(VisualElement durabilityRoot, Gradient durabilityGradient, BoatActionData boatData){
        _durabilityGradient = durabilityGradient;

        _circle = durabilityRoot.Q<VisualElement>("durability");
        _boatImage = durabilityRoot.Q<VisualElement>("boat-image");

        _boatData = boatData;
    }

    public void SetColor(){
        BoatData boatData = GameManager.Instance.GetManager<DataManager>().GetData(DataType.BoatData) as BoatData;

        float percent = _boatData.CurrentDurability / boatData.MaxDurablity;
        Color color = _durabilityGradient.Evaluate(1 - percent);
        color.a = 200f / 256f;

        _circle.style.backgroundColor = color;
        _boatImage.style.unityBackgroundImageTintColor = color;
    }
}
