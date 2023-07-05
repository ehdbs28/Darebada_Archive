using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FishingUpgradeUnit{
    public int ID = 0;
    public List<float> Value = new List<float>();
    public int MaxLevel = 0;
}

public class FishingUpgradeTable : LoadableData
{
    public int Size = 0;
    public DataTable<FishingUpgradeUnit> DataTable;

    public override void SetUp(string[] dataArr)
    {
        DataTable.Add(new FishingUpgradeUnit());

        DataTable[Size].ID = int.Parse(dataArr[0]);

        string[] arrayValue = dataArr[1].Split(",");    
        for(int i = 0; i < arrayValue.Length; i++){
            DataTable[Size].Value.Add(float.Parse(arrayValue[i]));
        }

        DataTable[Size].MaxLevel = int.Parse(dataArr[2]);

        ++Size;
    }
}
