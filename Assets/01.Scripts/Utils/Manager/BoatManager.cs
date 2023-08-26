using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour, IManager
{
    [SerializeField]
    private BoatDataUnit _currentBoatData;
    public BoatDataUnit CurrentBoatData => _currentBoatData;

    private BoatDataTable _dataTable;

    public void InitManager()
    {
        _dataTable = GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.BoatData) as BoatDataTable;

        _currentBoatData = (GameManager.Instance.GetManager<DataManager>().GetData(DataType.BoatData) as BoatData)
            ?.CurrentBoat;

        if (_currentBoatData.Name == "")
        {
            SelectBoat(0);
        }
    }

    public void SelectBoat(int idx){
        _currentBoatData = _dataTable.DataTable[idx];
        ((BoatData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.BoatData))
            .CurrentBoat = _currentBoatData;
        ((BoatData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.BoatData))
            .Fuel = _currentBoatData.MaxFuel;
    }

    public void ResetManager(){}
    public void UpdateManager(){}
}
