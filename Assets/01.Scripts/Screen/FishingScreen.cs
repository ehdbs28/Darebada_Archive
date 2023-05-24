using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FishingScreen : UIScreen
{
    private Label _heightText;
    private VisualElement _heightCursor;

    protected override void AddEvent(VisualElement root)
    {

    }

    protected override void FindElement(VisualElement root)
    {
        _heightCursor = root.Q<Label>("height-text");
        _heightCursor = root.Q<VisualElement>("cursor");
    }
}
