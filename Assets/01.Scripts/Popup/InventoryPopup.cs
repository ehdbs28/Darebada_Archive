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

    private List<VisualElement> _inventoryUnitElements = new List<VisualElement>();
    private VisualElement _selectedItem;
    private Quaternion _itemQuaternion;

    protected override void AddEvent(VisualElement root)
    {
        _exitBtn.RegisterCallback<ClickEvent>(e => {
            RemoveRoot();
        });

        foreach(VisualElement invenUnit in _inventoryUnitElements)
        {
            invenUnit.RegisterCallback<ClickEvent>(e =>
            {
                _selectedItem = invenUnit;
            });
        }

        _rightRotateBtn.RegisterCallback<ClickEvent>(e =>
        {
            _itemQuaternion = _selectedItem.transform.rotation;
            _selectedItem.transform.rotation = Quaternion.Euler(_itemQuaternion.x, _itemQuaternion.y, _itemQuaternion.z - 90f);
        });

        _leftRotateBtn.RegisterCallback<ClickEvent>(e =>
        {
            _itemQuaternion = _selectedItem.transform.rotation;
            _selectedItem.transform.rotation = Quaternion.Euler(_itemQuaternion.x, _itemQuaternion.y, _itemQuaternion.z - 90f);
        });
    }
    
    public override void RemoveEvent()
    {
    }

    protected override void FindElement(VisualElement root)
    {
        _exitBtn = root.Q<VisualElement>("exit-btn");
        _rightRotateBtn = root.Q<VisualElement>("rotate-right-btn");
        _leftRotateBtn = root.Q<VisualElement>("rotate-left-btn");
        _inventoryUnitElements = root.Query<VisualElement>(className: "inventory-unit").ToList();
    }
}
