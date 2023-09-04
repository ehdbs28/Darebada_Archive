using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class FishDataUnit{
    public string Name;
    public FishVisual Visual;
    public float Price;
    public OceanType Habitat;
    public int Rarity;
    public float Speed;
    public int InvenSizeX;
    public int InvenSizeY;
    public float MinLenght;
    public float MaxLenght;
    public float MinWeight;
    public float MaxWeight;
    public float SpawnPercent;
    public List<string> Favorites;
    public string Description;
}

public class FishDataTable : LoadableData
{
    public DataTable<FishDataUnit> DataTable = new DataTable<FishDataUnit>();

    private OceanType _prevOceanType = OceanType.RichOcean;
    private int _localFishCnt = 0;

    #if UNITY_EDITOR
    public override void AddData(string[] dataArr)
    {
        DataTable.Add(new FishDataUnit());

        DataTable[Size].Name = dataArr[0];
        DataTable[Size].Price = float.Parse(dataArr[2]);
        DataTable[Size].Habitat = (OceanType)Enum.Parse(typeof(OceanType), $"{dataArr[3]}Ocean");
        DataTable[Size].Rarity = int.Parse(dataArr[4]);
        DataTable[Size].Speed = float.Parse(dataArr[5]);
        DataTable[Size].InvenSizeX = int.Parse(dataArr[6]);
        DataTable[Size].InvenSizeY = int.Parse(dataArr[7]);
        DataTable[Size].MinLenght = float.Parse(dataArr[8]);
        DataTable[Size].MaxLenght = float.Parse(dataArr[9]);
        DataTable[Size].MinWeight = float.Parse(dataArr[10]);
        DataTable[Size].MaxWeight = float.Parse(dataArr[11]);
        DataTable[Size].SpawnPercent = float.Parse(dataArr[12]);

        if (_prevOceanType != DataTable[Size].Habitat)
        {
            _localFishCnt = 0;
            _prevOceanType = DataTable[Size].Habitat;
        }
        
        string path = $"Assets/06.SO/FishVisual/{(int)DataTable[Size].Habitat:D2}.{DataTable[Size].Habitat}/{_localFishCnt:D2}.{dataArr[1]}.asset";
        DataTable[Size].Visual = AssetDatabase.LoadAssetAtPath<FishVisual>(path);

        string[] favorites = dataArr[13].Split(",");
        DataTable[Size].Favorites = new List<string>();
        foreach(var f in favorites){
            DataTable[Size].Favorites.Add(f);
        }

        DataTable[Size].Description = dataArr[14];

        ++Size;
        ++_localFishCnt;
    }
    #endif

    public override void Clear()
    {
        base.Clear();
        DataTable.Clear();
    }
}
