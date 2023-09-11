using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UILetterUnit
{
    private VisualTreeAsset _templete;
    
    private ScrollView _parent;

    protected VisualElement _root;

    private Label _titleLabel;
    private Label _fromLabel;
    private Label _dateLabel;
    private Label _descLabel;

    private LetterUnit _unit;

    private bool _isOpen;
    public bool IsOpen => _isOpen;

    public UILetterUnit(VisualTreeAsset templete, ScrollView parent){
        _templete = templete;
        _parent = parent;
        _isOpen = false;
    }

    public virtual void Generate(LetterUnit unit){
        var templeteContainer = _templete.Instantiate();
        _root = templeteContainer.Q<VisualElement>("letter-unit");
        _parent.contentContainer.Add(_root);

        FindElement();
        Setting(unit);
        AddEvent();
    }

    private void Setting(LetterUnit unit)
    {
        _unit = unit;
        switch(unit.Type){
            case LetterType.Report:
                _root.AddToClassList("report");
            break;
            case LetterType.Thanks:
                _root.AddToClassList("thanks");
            break;
            case LetterType.Request:
                _root.AddToClassList("request");
            break;
        }

        _titleLabel.text = unit.Title;
        _fromLabel.text = unit.From;
        _dateLabel.text = $"{unit.Date.Month}월{unit.Date.Day}일";
        _descLabel.text = unit.Desc;
    }

    protected virtual void FindElement(){
        _titleLabel = _root.Q<Label>("title-text");
        _fromLabel = _root.Q<Label>("name-text");
        _dateLabel = _root.Q<Label>("date-text");
        _descLabel = _root.Q<Label>("desc-text");
    }

    protected virtual void AddEvent(){
        _root.RegisterCallback<ClickEvent>(e =>
        {
            Toggle(!_isOpen);
        });
    }

    public void Toggle(bool open){
        _isOpen = open;
        if(open){
            _root.AddToClassList("on");
            _root.AddToClassList("check");
        }
        else{
            _root.RemoveFromClassList("on");
        }
    }

    public void Remove(){
        _parent.Remove(_root);
        GameManager.Instance.GetManager<LetterManager>().RemoveLetter(_unit);
    }
}
