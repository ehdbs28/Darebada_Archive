using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUnit
{
    public VisualElement unit;
    public int index;
    public Vector3 pos;

    public InventoryUnit(VisualElement unit, int index, Vector3 pos)
    {
        this.unit = unit;
        this.index = index;
        this.pos = pos;
    }
}

public class InventoryPopup : UIPopup
{
    // 준호 구현 방식 따라서 달라질 듯
    private VisualElement _exitBtn;
    private VisualElement _rightRotateBtn;
    private VisualElement _leftRotateBtn;

    private List<VisualElement> _fishItems = new List<VisualElement>();
    private VisualElement _selectedItem;
    private Quaternion _itemQuaternion;

    private bool _isSelected = false;
    private Vector2 _itemPos;

    private VisualElement _selectedObj;

    private bool _itemMove = false;
    private List<InventoryUnit> _inventoryUnits = new List<InventoryUnit>();

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0) && _selectedItem != null)
        //{
        //    Vector2 mousePos = GameManager.Instance.GetManager<InputManager>().MousePosition;
        //    Debug.Log(mousePos);
        //    mousePos = RuntimePanelUtils.ScreenToPanel(_root.panel, mousePos);
        //    Debug.Log(mousePos);
        //}
    }

    protected override void AddEvent(VisualElement root)
    {
        _exitBtn.RegisterCallback<ClickEvent>(e => {
            RemoveRoot();
        });

        foreach (VisualElement fishItem in _fishItems)
        {
            fishItem.RegisterCallback<ClickEvent>(e =>
            {
                //나중에 방생/이동 팝업 추가되면 띄워주는 코드 작성해야함.
                _selectedItem = fishItem;
                _itemMove = true;
                StyleBackground stBackground = _selectedItem.resolvedStyle.backgroundImage;
                _selectedObj.style.backgroundImage = stBackground;
            });
        }

        _rightRotateBtn.RegisterCallback<ClickEvent>(e =>
        {
            if (_selectedItem != null && _itemMove)
            {
                Vector3 newEuler = new Vector3();
                Quaternion currentRotation = new Quaternion();
                newEuler += new Vector3(0, 0, _selectedItem.transform.rotation.eulerAngles.z - 90);
                currentRotation.eulerAngles = newEuler;
                _selectedItem.transform.rotation = currentRotation;
            }
        });

        _leftRotateBtn.RegisterCallback<ClickEvent>(e =>
        {
            if(_selectedItem != null && _itemMove)
            {
                Vector3 newEuler = new Vector3();
                Quaternion currentRotation = new Quaternion();
                newEuler += new Vector3(0, 0, _selectedItem.transform.rotation.eulerAngles.z + 90);
                currentRotation.eulerAngles = newEuler;
                _selectedItem.transform.rotation = currentRotation;
            }
        });

        //foreach (VisualElement unit in _inventoryUnits)
        //{
        //    unit.RegisterCallback<ClickEvent>(e =>
        //    {
        //        //if(_selectedItem != null && _itemMove)
        //        //{
        //        //    Vector3 newPos = new Vector3();
        //        //    newPos = new Vector3(_inventoryUnits.BinarySearch(unit) % 7 * 100,
        //        //        _inventoryUnits.BinarySearch(unit) % 8 * 100,
        //        //        _selectedItem.transform.position.z);
        //        //    _selectedItem.transform.position += newPos;
        //        //}

        //        Debug.Log(_inventoryUnits.BinarySearch(unit));
        //    });
        //}
        int i = 0;
        foreach(var unit in _inventoryUnits)
        {
            unit.index = i;
            unit.pos = new Vector3(i / 8 * 100, i % 8 * 100);

            unit.unit.RegisterCallback<ClickEvent>(e =>
            {
                if (_selectedObj.style.backgroundImage != null)
                {
                    _selectedItem.transform.position = unit.pos;
                    Debug.Log(unit.pos);
                }
            });

            ++i;
        }
    }
    
    public override void RemoveEvent()
    {
    }

    protected override void FindElement(VisualElement root)
    {
        _exitBtn = root.Q<VisualElement>("exit-btn");
        _rightRotateBtn = root.Q<VisualElement>("rotate-right-btn");
        _leftRotateBtn = root.Q<VisualElement>("rotate-left-btn");
        _fishItems = root.Query<VisualElement>(className: "fish-item").ToList();
        _selectedObj = root.Q<VisualElement>("inner");

        List<VisualElement> units = root.Query<VisualElement>(className: "inventory-unit").ToList();
        foreach(var unit in units)
        {
            _inventoryUnits.Add(new InventoryUnit(unit, 0, Vector3.zero));
        }
    }
}