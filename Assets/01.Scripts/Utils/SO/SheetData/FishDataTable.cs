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
    public OceneType Habitat;
    public int Rarity;
    public float Speed;
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

    public override void AddData(string[] dataArr)
    {
        DataTable.Add(new FishDataUnit());

        DataTable[Size].Name = dataArr[0];

        // Visual은 후에 추가

        DataTable[Size].Price = float.Parse(dataArr[1]);
        DataTable[Size].Habitat = (OceneType)Enum.Parse(typeof(OceneType), $"{dataArr[2]}Ocene");
        DataTable[Size].Rarity = int.Parse(dataArr[3]);
        DataTable[Size].Speed = float.Parse(dataArr[4]);
        DataTable[Size].MinLenght = float.Parse(dataArr[5]);
        DataTable[Size].MaxLenght = float.Parse(dataArr[6]);
        DataTable[Size].MinWeight = float.Parse(dataArr[7]);
        DataTable[Size].MaxWeight = float.Parse(dataArr[8]);
        DataTable[Size].SpawnPercent = float.Parse(dataArr[9]);

        string[] favorites = dataArr[10].Split(",");
        DataTable[Size].Favorites = new List<string>();
        foreach(var f in favorites){
            DataTable[Size].Favorites.Add(f);
        }

        DataTable[Size].Description = dataArr[11];

        ++Size;
    }

    public override void Clear()
    {
        base.Clear();
        DataTable.Clear();
    }
}
