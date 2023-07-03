using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBoatFuel
{
    private Gradient _durabilityGradient;
    
    private VisualElement _circle;
    private VisualElement _boatImage;

    private BoatController _boatController;

    public UIBoatFuel(VisualElement durabilityRoot, Gradient durabilityGradient, BoatController boatController){
        _durabilityGradient = durabilityGradient;

        _circle = durabilityRoot.Q<VisualElement>("durability");
        _boatImage = durabilityRoot.Q<VisualElement>("boat-image");

        _boatController = boatController;
    }

    public void SetColor(){
        BoatData boatData = GameManager.Instance.GetManager<DataManager>().GetData(DataType.BoatData) as BoatData;

        float percent = _boatController.BoatActionData.CurrentFuel / _boatController.DataSO.MaxFuel;
        Color color = _durabilityGradient.Evaluate(1 - percent);
        color.a = 200f / 256f;

        _circle.style.backgroundColor = color;
        _boatImage.style.unityBackgroundImageTintColor = color;
    }
}
