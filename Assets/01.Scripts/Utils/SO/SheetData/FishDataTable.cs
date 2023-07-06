using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FishViual{
    public Mesh VisualMesh;
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
    public float SpawnPercent;
}

public class FishDataTable : LoadableData
{
    public int Size = 0;
    public DataTable<FishDataUnit> DataTable = new DataTable<FishDataUnit>();

    public override void Clear()
    {
        Size = 0;
        DataTable.Clear();
    }

    public override void AddData(string[] dataArr)
    {
        DataTable.Add(new FishDataUnit());

        DataTable[Size].Name = dataArr[0];

        // Visual은 후에 추가

        DataTable[Size].Price = float.Parse(dataArr[1]);
        DataTable[Size].Habitat = (OceneType)Enum.Parse(typeof(OceneType), $"{dataArr[2]}Ocene");
        DataTable[Size].Rarity = int.Parse(dataArr[3]);
        DataTable[Size].Speed = float.Parse(dataArr[4]);
        DataTable[Size].SpawnPercent = float.Parse(dataArr[5]);

        ++Size;
    }
}
