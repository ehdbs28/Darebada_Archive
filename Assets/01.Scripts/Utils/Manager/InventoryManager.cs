using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryManager : IManager
{
    //���� �ӽ÷� �� �־���� ����. ���� ũ��� �ٲ����.
    public readonly int boardSizeX = 7;
    public readonly int boardSizeY = 8;

    public readonly int[] destX = { -1, 1, 0, 0 };
    public readonly int[] destY = { 0, 0, -1, 1 };

    public InventoryTile[,] Tiles;
    public List<InventoryUnit> Units;
    
    public void InitManager()
    {
        Tiles = new InventoryTile[boardSizeY, boardSizeX];
        Units = new List<InventoryUnit>();

        for(int i = 0; i < boardSizeY; i++)
        {
            for(int j = 0; j < boardSizeX; j++)
            {
                Tiles[i, j] = new InventoryTile
                {
                    IsFull = false,
                    xIdx = j,
                    yIdx = i
                };
            }
        }
    }
    
    public void AddUnit(FishDataUnit unitData)
    {
        VisualTreeAsset template = ((InventoryPopup)GameManager.Instance.GetManager<UIManager>().GetPanel(PopupType.Inventory)).UnitTemplate;
        VisualElement parent = ((InventoryPopup)GameManager.Instance.GetManager<UIManager>().GetPanel(PopupType.Inventory)).UnitParent;
        InventoryUnit newUnit = new InventoryUnit(template, parent, unitData)
        {
            PosX = -500,
            PosY = 0,
            Rotate = 0
        };
        Units.Add(newUnit);
        ((InventoryPopup)GameManager.Instance.GetManager<UIManager>().GetPanel(PopupType.Inventory))._selectedUnit = newUnit;
        GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.Inventory);
    }


    public void ResetManager()
    {
        
    }

    public void UpdateManager()
    {

    }
}
