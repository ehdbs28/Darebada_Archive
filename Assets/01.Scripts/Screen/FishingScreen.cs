using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FishingScreen : UIScreen
{
    private Label _heightText;
    private VisualElement _heightCursor;

    public override void AddEvent()
    {

    }

    public override void RemoveEvent()
    {
    }

    public override void FindElement()
    {
        _heightText = _root.Q<Label>("height-text");
        _heightCursor = _root.Q<VisualElement>("cursor");
    }

    public void SetHeight(float percent, float height){
        _heightCursor.style.top = new StyleLength(new Length(percent * 100, LengthUnit.Percent));
        _heightText.text = $"{(int)height}M";
    }
}
