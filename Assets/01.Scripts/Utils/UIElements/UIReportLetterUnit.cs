using UnityEngine;
using UnityEngine.UIElements;

public class UIReportLetterUnit : UILetterUnit
{
    private Label _valueLabel;
    private string _value;
    
    public UIReportLetterUnit(VisualTreeAsset templete, ScrollView parent, string value) : base(templete, parent)
    {
        _value = value;
    }

    public override void Generate(LetterUnit unit)
    {
        base.Generate(unit);
        _valueLabel.text = _value;
    }

    protected override void FindElement()
    {
        base.FindElement();
        _valueLabel = _root.Q<Label>("value-text");
    }
}