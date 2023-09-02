using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoatDataUnit{
    public string Name;
    public int Id;
    public float Price;
    public BoatViual Visual;
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
        DataTable[Size].Price = float.Parse(dataArr[1]);
        
        // set visual
        
        DataTable[Size].MaxSpeed = float.Parse(dataArr[2]);
        DataTable[Size].ForwardAcceleration = float.Parse(dataArr[3]);
        DataTable[Size].BackwardAcceleration = float.Parse(dataArr[4]);
        DataTable[Size].Deceleration = float.Parse(dataArr[5]);
        DataTable[Size].RotationSpeed = float.Parse(dataArr[6]);
        DataTable[Size].RotationAcceleration = float.Parse(dataArr[7]);
        DataTable[Size].RotationDeceleration = float.Parse(dataArr[8]);
        DataTable[Size].MaxFuel = float.Parse(dataArr[9]);

        ++Size;
    }

    public override void Clear()
    {
        base.Clear();
        DataTable.Clear();
    }
}
