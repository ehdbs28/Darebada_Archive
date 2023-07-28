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
    
    public void EquipItem(FishingItemType type)
    {
        Debug.Log(type);
        FishingData data = (FishingData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.FishingData);

        if (type == FishingItemType.None)
        {
            data.CurSelectedItem = type;
            return;
        }
        
        if (data.ItemVal[(int)type] <= 0)
            return;

        data.ItemVal[(int)type]--;
        data.CurSelectedItem = type;
    }

    public void ResetManager()
    {
        
    }
}
