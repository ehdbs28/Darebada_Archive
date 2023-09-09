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

    private InventoryData _data;

    public void InitManager()
    {
        _data = (InventoryData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.InventoryData);
    }
    
    public void AddUnit(FishDataUnit unitData, Vector2 size)
    {
        InventoryUnit unit = new InventoryUnit(unitData, size);

        if (CheckPosition(_data, unit))
        {
            _data?.Units.List.Add(unit);
        }
        else
        {
            Debug.Log("인벤에 자리가 없음");
        }
    }

    private bool CheckPosition(InventoryData data, InventoryUnit unit)
    {
        for (int y = 0; y <= BoardSizeY - unit.size.y; y++)
        {
            for (int x = 0; x <= BoardSizeX - unit.size.x; x++)
            {
                var minX = x;
                var maxX = (x - 1) + (int)unit.size.x;
                var minY = y;
                var maxY = (y - 1) + (int)unit.size.y;

                if (Search(unit, minX, maxX, minY, maxY))
                {
                    unit.posX = x;
                    unit.posY = y;
                    return true;
                }
            }
        }

        return false;
    }
    
    public bool Search(InventoryUnit selectedUnit, int minX, int maxX, int minY, int maxY)
    {
        if (minX < 0 
            || maxX > InventoryManager.BoardSizeX 
            || minY < 0 
            || maxY > InventoryManager.BoardSizeY)
            return false;
        
        foreach (var unit in _data.Units.List)
        {
            if(unit != selectedUnit)
            {
                if ((minX >= unit.MinX && maxX <= unit.MaxX)
                    && (minY >= unit.MinY && maxY <= unit.MaxY))
                {
                    return false;
                }
            }
        }

        return true;
    }
    
    public void ResetManager(){}
    public void UpdateManager(){}
}
