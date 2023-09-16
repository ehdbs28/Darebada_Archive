using UnityEngine.UIElements;

public class UIFishAddContent
{
    private VisualElement _root;
    private VisualTreeAsset _unitTemplete;
    private Fishbowl _fishbowl;

    private VisualElement _fishAddBtn;
    private VisualElement _fishContents;

    private InventoryUnit _selectedUnit;
    
    public UIFishAddContent(VisualElement root, VisualTreeAsset unitTemplete, Fishbowl fishbowl)
    {
        _selectedUnit = null;
        
        _root = root;
        _unitTemplete = unitTemplete;
        _fishbowl = fishbowl;

        FindElement();
        AddEvent();
    }

    private void FindElement()
    {
        _fishAddBtn = _root.Q("addfish-btn");
        _fishContents = _root.Q("fish-contents");

        var fishData = (InventoryData)GameManager.Instance.GetManager<DataManager>()
            .GetData(DataType.InventoryData);
        fishData.Units.List.ForEach(unit =>
        {
            VisualElement unitElem = _unitTemplete.Instantiate();
            unitElem = unitElem.Q("inventory-unit");
            
            unitElem.style.width = unit.data.InvenSizeX * 120;
            unitElem.style.height = unit.data.InvenSizeY * 120;
            unitElem.style.left = 59 + (120 * unit.data.InvenSizeX);
            unitElem.style.top = 21 + (120 * unit.data.InvenSizeY);
            
            unit.Generate(unitElem, false);
            _fishContents.Add(unitElem);
            
            unitElem.RegisterCallback<ClickEvent>(e =>
            {
                _selectedUnit = unit;
            });
        });
    }

    private void AddEvent()
    {
        _fishAddBtn.RegisterCallback<ClickEvent>(e =>
        {
            if (_selectedUnit != null)
            {
                _fishbowl.AddFIsh(_selectedUnit.data);
                ((InventoryData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.InventoryData))
                    .Units.List.Remove(_selectedUnit);
                _fishContents.Remove(_selectedUnit.Root);
                _selectedUnit = null;
            }
        });
    }
}
