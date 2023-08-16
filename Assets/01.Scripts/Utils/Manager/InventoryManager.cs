using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryManager : IManager
{
    public const int BoardSizeX = 7;
    public const int BoardSizeY = 8;

    public void InitManager()
    {
        
    }
    
    public void AddUnit(FishDataUnit unitData, Vector2 size)
    {
        InventoryData data = GameManager.Instance.GetManager<DataManager>().GetData(DataType.InventoryData) as InventoryData;
        if (data != null) 
            data.Units.List.Add(new InventoryUnit(unitData, size));
    }


    public void ResetManager()
    {
        
    }

    public void UpdateManager()
    {

    }
}
