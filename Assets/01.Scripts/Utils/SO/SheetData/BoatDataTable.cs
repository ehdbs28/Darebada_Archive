using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class BoatDataUnit{
    public string Name;
    public int Price;
    public BoatVisual Visual;
    public float MaxSpeed;
    public float ForwardAcceleration;
    public float BackwardAcceleration;
    public float Deceleration;
    public float RotationSpeed;
    public float RotationAcceleration;
    public float RotationDeceleration;
    public float MaxFuel;
}

public class BoatDataTable : LoadableData
{
    public DataTable<BoatDataUnit> DataTable = new DataTable<BoatDataUnit>();

    public override void AddData(string[] dataArr)
    {
        DataTable.Add(new BoatDataUnit());

        DataTable[Size].Name = dataArr[0];
        DataTable[Size].Price = int.Parse(dataArr[2]);
        
        DataTable[Size].MaxSpeed = float.Parse(dataArr[3]);
        DataTable[Size].ForwardAcceleration = float.Parse(dataArr[4]);
        DataTable[Size].BackwardAcceleration = float.Parse(dataArr[5]);
        DataTable[Size].Deceleration = float.Parse(dataArr[6]);
        DataTable[Size].RotationSpeed = float.Parse(dataArr[7]);
        DataTable[Size].RotationAcceleration = float.Parse(dataArr[8]);
        DataTable[Size].RotationDeceleration = float.Parse(dataArr[9]);
        DataTable[Size].MaxFuel = float.Parse(dataArr[10]);

        string path = $"Assets/06.SO/BoatVisual/{Size:D2}.{dataArr[1]}.asset";
        DataTable[Size].Visual = AssetDatabase.LoadAssetAtPath<BoatVisual>(path);
        
        ++Size;
    }

    public override void Clear()
    {
        base.Clear();
        DataTable.Clear();
    }
}
