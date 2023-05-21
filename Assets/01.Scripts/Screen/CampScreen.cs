using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CampScreen : UIScreen
{
    private Label _timeText;
    private Label _dateText;
    private Label _goldText;

    private VisualElement _settingBtn;
    private VisualElement _letterBtn;
    private VisualElement _dictionaryBtn;

    protected override void AddEvent(VisualElement root)
    {
    }

    protected override void FindElement(VisualElement root)
    {
        _timeText = root.Q<Label>("time-text");
        _dateText = root.Q<Label>("date-text");
        _goldText = root.Q("money-container").Q<Label>("text");

        _settingBtn = root.Q<VisualElement>("setting-btn");
        _letterBtn = root.Q<VisualElement>("letter-btn");
        _dictionaryBtn = root.Q<VisualElement>("dictionary-btn");
    }
}
