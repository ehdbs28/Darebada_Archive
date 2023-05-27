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
        _exitBtn.RegisterCallback<ClickEvent>(e => {
            RemoveRoot();
        });

        foreach(VisualElement letter in _letters)
        {
            letter.RegisterCallback<ClickEvent>(e =>
            {
                letter.AddToClassList("on");
                letter.AddToClassList("check");
            });
        }

        _selectAllToggle.RegisterCallback<ClickEvent>(e =>
        {
            foreach(VisualElement letter in _letters)
            {
                letter.AddToClassList("on");
            }
        });

        _exitBtn.RegisterCallback<ClickEvent>(e =>
        {
            RemoveRoot();
        });

        _deleteBtn.RegisterCallback<ClickEvent>(e =>
        {
            foreach(VisualElement letter in _letters)
            {
                if (letter.ClassListContains("on"))
                {
                    letter.RemoveFromHierarchy();
                }
            }
        });
    }

    protected override void FindElement(VisualElement root)
    {
        _exitBtn = root.Q<VisualElement>("exit-btn");
        _deleteBtn = root.Q<VisualElement>("delete-btn");
        _selectAllToggle = root.Q<VisualElement>("select-all-toggle").Q<VisualElement>("inner");

        _letters = root.Query<VisualElement>(className: "letter-unit").ToList();
    }
}
