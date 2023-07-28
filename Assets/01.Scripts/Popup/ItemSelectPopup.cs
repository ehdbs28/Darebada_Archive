using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemSelectPopup : UIPopup
{
    private VisualElement _exitBtn;

    private List<UISelectItemUnit> _units;
    
    public override void AddEvent()
    {
        _exitBtn.RegisterCallback<ClickEvent>(e =>
        {
            RemoveEvent();
            RemoveRoot();
        });
    }

    public override void RemoveEvent()
    {
    }

    public override void FindElement()
    {
        _exitBtn = _root.Q<VisualElement>("exit-btn");

        List<VisualElement> unitElements = _root.Query<VisualElement>(className: "select-item").ToList();
        ShopItemDataTable table = (ShopItemDataTable)GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.ShopItemData);
        _units = new List<UISelectItemUnit>();
        
        for (int i = 0; i < unitElements.Count; i++)
        {
            _units.Add(new UISelectItemUnit(unitElements[i], table.DataTable[i]));
        }
    }
}
