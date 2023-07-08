using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingUpgradeManager : IManager
{
    public void InitManager()
    {
    }

    public void Upgrade(int idx){
        FishingData fishingData = GameManager.Instance.GetManager<DataManager>().GetData(DataType.FishingData) as FishingData;
        
        if(idx == 0){
            GameManager.Instance.GetManager<MoneyManager>().Payment(
                GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.FishingUpgradeData);
            );
            fishingData.StringLength_Level++;
        }
        else if(idx == 1){
            fishingData.StringStrength_Level++;
        }
        else{
            fishingData.ThrowPower_Level++;
        }
    }

    public void UpdateManager(){}
    public void ResetManager(){}
}
