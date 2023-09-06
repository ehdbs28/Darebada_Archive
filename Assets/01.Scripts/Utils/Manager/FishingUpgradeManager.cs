using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingUpgradeManager : IManager
{
    FishingUpgradeTable _dataTable;

    public void InitManager()
    {
        _dataTable = GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.FishingUpgradeData) as FishingUpgradeTable;
    }

    public void Upgrade(int idx){
        FishingData fishingData = GameManager.Instance.GetManager<DataManager>().GetData(DataType.FishingData) as FishingData;
        
        if(idx == 0){
            if(fishingData.StringLength_Level >= _dataTable.DataTable[idx].MaxLevel)
                return;

            GameManager.Instance.GetManager<MoneyManager>().Payment(
                _dataTable.DataTable[idx].Price[fishingData.StringLength_Level],
                () => fishingData.StringLength_Level++
            );
        }
        else if(idx == 1){
            if(fishingData.StringStrength_Level >= _dataTable.DataTable[idx].MaxLevel)
                return;

            GameManager.Instance.GetManager<MoneyManager>().Payment(
                _dataTable.DataTable[idx].Price[fishingData.StringStrength_Level],
                () => fishingData.StringStrength_Level++
            );
            
        }
        else{
            if(fishingData.ThrowPower_Level >= _dataTable.DataTable[idx].MaxLevel)
                return;

            GameManager.Instance.GetManager<MoneyManager>().Payment(
                _dataTable.DataTable[idx].Price[fishingData.ThrowPower_Level],
                () => fishingData.ThrowPower_Level++    
            );  
        }
    }

    public void UpdateManager(){}
    public void ResetManager(){}
}
