using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour, IManager
{
    public static InventoryManager Instance;

    //대충 임시로 수 넣어놓은 거임. 실제 크기로 바꿔야함.
    public int Board_Size_X = 7;
    public int Board_Size_Y = 8;

    public int[] destX = { -1, 1, 0, 0 };
    public int[] destY = { 0, 0, -1, 1 };

    public InventoryTile[,] Tiles;
    public List<InventoryUnit> Units;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        Tiles = new InventoryTile[Board_Size_Y, Board_Size_X];
        Units = new List<InventoryUnit>();

        for(int i = 0; i < Board_Size_Y; i++)
        {
            for(int j = 0; j < Board_Size_X; j++)
            {
                Tiles[i, j] = new InventoryTile();
                Tiles[i, j].IsFull = false;
                Tiles[i, j].xIdx = j;
                Tiles[i, j].yIdx = i;
            }
        }
    }

    public void AddUnit(FishDataUnit unitData)
    {
        VisualTreeAsset template = ((InventoryPopup)GameManager.Instance.GetManager<UIManager>().GetPanel(PopupType.Inventory)).UnitTemplate;
        VisualElement parent = ((InventoryPopup)GameManager.Instance.GetManager<UIManager>().GetPanel(PopupType.Inventory)).UnitParent;
        InventoryUnit newUnit = new InventoryUnit(template, parent, unitData);
        newUnit.PosX = -500;
        newUnit.PosY = 0;
        newUnit.Rotate = 0;
        Units.Add(newUnit);
        ((InventoryPopup)GameManager.Instance.GetManager<UIManager>().GetPanel(PopupType.Inventory))._selectedUnit = newUnit;
        GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.Inventory);
    }

    public void InitManager()
    {
        ResetManager();
    }

    public void ResetManager()
    {
        
    }

    public void UpdateManager()
    {

    }
}
