using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : IManager
{
    public BoatDataUnit CurrentBoatUnit{
        get{
            return (GameManager.Instance.GetManager<DataManager>().GetData(DataType.BoatData) as BoatData).CurrentBoat;
        }
        private set{
            (GameManager.Instance.GetManager<DataManager>().GetData(DataType.BoatData) as BoatData).CurrentBoat = value;
        }
    }

    public void SelectBoat(int idx){
        BoatDataTable table = GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.BoatData) as BoatDataTable;
        CurrentBoatUnit = table.DataTable[idx];
    }

    public void ResetManager(){}
    public void UpdateManager(){}
    public void InitManager(){}
}
