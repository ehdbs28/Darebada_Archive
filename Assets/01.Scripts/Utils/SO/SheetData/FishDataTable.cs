using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FishViual{
    public Mesh VisualMesh;
    public Sprite Profile;
    public Material MainMat;
}

[Serializable]
public class FishDataUnit{
    public string Name;
    public FishViual Visual;
    public float Price;
    public OceanType Habitat;
    public int Rarity;
    public float Speed;
    public int InvenSizeX;
    public int InvenSizeY;
    public float MinLength;
    public float MaxLength;
    public float MinWeight;
    public float MaxWeight;
    public float SpawnPercent;
    public List<string> Favorites;
    public string Description;
}

public class FishDataTable : LoadableData
{
    public DataTable<FishDataUnit> DataTable = new DataTable<FishDataUnit>();

    public override void AddData(string[] dataArr)
    {
        DataTable.Add(new FishDataUnit());

        DataTable[Size].Name = dataArr[0];

        // Visual은 후에 추가

        DataTable[Size].Price = float.Parse(dataArr[1]);
        DataTable[Size].Habitat = (OceanType)Enum.Parse(typeof(OceanType), $"{dataArr[2]}Ocean");
        DataTable[Size].Rarity = int.Parse(dataArr[3]);
        DataTable[Size].Speed = float.Parse(dataArr[4]);
        DataTable[Size].InvenSizeX = int.Parse(dataArr[5]);
        DataTable[Size].InvenSizeY = int.Parse(dataArr[6]);
        DataTable[Size].MinLength = float.Parse(dataArr[7]);
        DataTable[Size].MaxLength = float.Parse(dataArr[8]);
        DataTable[Size].MinWeight = float.Parse(dataArr[9]);
        DataTable[Size].MaxWeight = float.Parse(dataArr[10]);
        DataTable[Size].SpawnPercent = float.Parse(dataArr[11]);

        string[] favorites = dataArr[12].Split(",");
        DataTable[Size].Favorites = new List<string>();
        foreach(var f in favorites){
            DataTable[Size].Favorites.Add(f);
        }

        DataTable[Size].Description = dataArr[13];

        ++Size;
    }

    public override void Clear()
    {
        base.Clear();
        DataTable.Clear();
    }
}
