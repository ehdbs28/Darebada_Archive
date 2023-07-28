using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBuyContent : UIPopupContent
{
    private List<VisualElement> _items;

    public UIBuyContent(VisualElement root, int index) : base(root, index)
    {
        ShopItemDataTable table = GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.ShopItemData) as ShopItemDataTable;

        for(int i = 0; i < table.Size; i++){
            _buyContent.Add(new UIShopUnit(
                _items[i],
                table.DataTable[i]
            ));
        }
    }

    protected override void FindElement()
    {
        base.FindElement();
        _items = _root.Query(className: "buy-item").ToList();
    }
}
