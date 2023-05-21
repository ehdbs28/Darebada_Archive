using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LetterPopup : UIPopup
{
    private VisualElement _exitBtn;
    private VisualElement _deleteBtn;
    private VisualElement _selectAllToggle;

    private List<VisualElement> _letters = new List<VisualElement>();

    protected override void AddEvent(VisualElement root)
    {
    }

    protected override void FindElement(VisualElement root)
    {
        _exitBtn = root.Q<VisualElement>("exit-btn");
        _deleteBtn = root.Q<VisualElement>("delete-btn");
        _selectAllToggle = root.Q<VisualElement>("select-all-toggle").Q<VisualElement>("inner");

        _letters = root.Query<VisualElement>(className: "letter-unit").ToList();
    }
}
