using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OceneScreen : UIScreen
{
    private Label _timeText;
    private Label _dateText;

    private VisualElement _settingBtn;
    private VisualElement _gobackBtn;
    private VisualElement _letterBtn;
    private VisualElement _dictionaryBtn;
    private VisualElement _inventoryBtn;

    protected override void AddEvent(VisualElement root)
    {
        
    }

    protected override void FindElement(VisualElement root)
    {
        _timeText = root.Q<Label>("time-text");
        _dateText = root.Q<Label>("date-text");

        _settingBtn = root.Q<VisualElement>("setting-btn");
        _gobackBtn = root.Q<VisualElement>("goto-btn");
        _letterBtn = root.Q<VisualElement>("letter-btn");
        _dictionaryBtn = root.Q<VisualElement>("dictionary-btn");
        _inventoryBtn = root.Q<VisualElement>("inventory-btn");
    }

    public void OnChangedTime(int hour, int minute, int second){
        
    }
}
