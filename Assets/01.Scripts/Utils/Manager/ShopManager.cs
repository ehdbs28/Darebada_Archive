using System.Collections;
using System.Collections.Generic;
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

    public void PurchaseItem(int idx)
    {
        if (_itemStock[idx] == 0)
            return;

        GameManager.Instance.GetManager<MoneyManager>().Payment(_dataTable.DataTable[idx].Price, () =>
        {
            _itemStock[idx]--;
            ((FishingData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.FishingData)).ItemVal[idx]++;
        });
    }

    public bool IsInStock(int idx)
    {
        return _itemStock[idx] > 0;
    }

    private void ReStockHandle(int year = 0, int month = 0, int day = 0)
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
