using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryPopup : UIPopup
{
    // 준호 구현 방식 따라서 달라질 듯
    private VisualElement _exitBtn;
    private VisualElement _rightRotateBtn;
    private VisualElement _leftRotateBtn;


    protected override void AddEvent(VisualElement root)
    {
    }

    protected override void FindElement(VisualElement root)
    {
        _exitBtn = root.Q<VisualElement>("exit-btn");
        _rightRotateBtn = root.Q<VisualElement>("rotate-right-btn");
        _leftRotateBtn = root.Q<VisualElement>("rotate-left-btn");
    }
}
