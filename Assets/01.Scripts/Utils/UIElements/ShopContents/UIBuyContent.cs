using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBuyContent : UIPopupContent
{
    private ScrollView _contentParent;

    private ShopItemDataTable _table;

    public UIBuyContent(VisualElement root, int index, VisualTreeAsset itemTemplete) : base(root, index)
    {
        _table = GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.ShopItemData) as ShopItemDataTable;

        for(int i = 0; i < _table.Size; i++){
            _buyContent.Add(new UIShopUnit(
                _contentParent,
                itemTemplete,
                _table.DataTable[i]
            ));
        }

        for(int i = 0; i < _table.Size; i++){
            ((UIShopUnit)_buyContent[i]).Generate();
        }
    }

    protected override void FindElement()
    {
        base.FindElement();
        _contentParent = _root.Q<ScrollView>("buy-items");
    }
}
