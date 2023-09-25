using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleteItemManager : IManager
{
    public void InitManager()
    {
        
    }

    public void UpdateManager()
    {
        
    }
    
    public bool EquipItem(FishingItemType type)
    {
        FishingData data = (FishingData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.FishingData);

        if (type == FishingItemType.None)
        {
            data.CurSelectedItem = type;
            return false;
        }
        
        if (data.ItemVal[(int)type] <= 0)
            return false;

        data.ItemVal[(int)type]--;
        data.CurSelectedItem = type;

        return true;
    }

    public void ResetManager()
    {
        
    }
}
