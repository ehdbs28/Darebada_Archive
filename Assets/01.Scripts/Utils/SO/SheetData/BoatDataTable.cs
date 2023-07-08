using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoatViual{
    public Mesh VisualMesh;
    public Material MainMat;
}

[System.Serializable]
public class BoatDataUnit{
    public string Name;
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

public class BoatDataTable : LoadableData<BoatDataUnit>
{
    public override void AddData(string[] dataArr)
    {
        DataTable.Add(new BoatDataUnit());

        DataTable[Size].Name = dataArr[0];
        DataTable[Size].Price = float.Parse(dataArr[1]);
        
        // 나중에 visual은 어드레서블로 받아와보자

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
}
