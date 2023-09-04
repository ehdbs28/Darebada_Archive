using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TitleScreen : UIScreen
{
    private VisualElement _titleContainer;
    private Label _tapToStart;
    public Label TapToStart => _tapToStart;
    
    public void SetAlpha(float value)
    {
        _titleContainer.style.opacity = value;
    }
    
    public override void AddEvent()
    {
    }

    public override void RemoveEvent()
    {
    }

    public override void FindElement()
    {
        _titleContainer = _root.Q("title-container");
        _tapToStart = _root.Q<Label>("tap-to-start");
    }
}
