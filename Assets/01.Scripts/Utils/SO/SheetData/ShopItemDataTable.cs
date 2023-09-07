using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopItemUnit
{
    public int Index;
    public string Name;
    public Sprite Image;
    public string Desc;
    public int Price;
    public int Durability;
}

public class ShopItemDataTable : LoadableData
{
    public DataTable<ShopItemUnit> DataTable = new DataTable<ShopItemUnit>();

    public override void AddData(string[] dataArr)
    {
        DataTable.Add(new ShopItemUnit());

        DataTable[Size].Index = Size;
        DataTable[Size].Name = dataArr[0];

        // Image는 나중에 가져오기

        DataTable[Size].Desc = dataArr[1];
        DataTable[Size].Price = int.Parse(dataArr[2]);
        DataTable[Size].Durability = int.Parse(dataArr[3]);

        Size++;
    }

    public override void Clear()
    {
        base.Clear();
        DataTable.Clear();
    }
}
