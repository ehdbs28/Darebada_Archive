using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class ShopManager : IManager
{
    private int[] _itemStock;
    private ShopItemDataTable _dataTable;
    
    public void InitManager()
    {
        _dataTable = ((ShopItemDataTable)GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.ShopItemData));
        _itemStock = new int[_dataTable.Size];
        
        ReStockHandle();
        GameManager.Instance.GetManager<TimeManager>().OnDayChangedEvent += ReStockHandle;
    }

    public void UpdateManager()
    {
    }

    public bool PurchaseItem(int idx)
    {
        if (_itemStock[idx] == 0)
            return false;

        bool success = false;
        GameManager.Instance.GetManager<MoneyManager>().Payment(_dataTable.DataTable[idx].Price, () =>
        {
            _itemStock[idx]--;
            ((FishingData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.FishingData)).ItemVal[idx]++;
            success = true;
        });

        return success;
    }

    public bool IsInStock(int idx)
    {
        return _itemStock[idx] > 0;
    }

    private void ReStockHandle(GameDate gameDate = null)
    {
        for (int i = 0; i < _dataTable.Size; i++)
        {
            if(_itemStock[i] >= 5)
                continue;

            _itemStock[i] += Random.Range(5, 10);
        }
    }
    
    public void ResetManager()
    {
    }
}
