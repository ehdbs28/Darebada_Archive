using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FishingUpgradeUnit{
    public List<float> Price = new List<float>();
    public List<float> Value = new List<float>();
    public int MaxLevel = 0;
}

public class FishingUpgradeTable : LoadableData
{
    public int Size = 0;
    public DataTable<FishingUpgradeUnit> DataTable = new DataTable<FishingUpgradeUnit>();

    public override void Clear()
    {
        Size = 0;
        DataTable.Clear();
    }

    public override void AddData(string[] dataArr)
    {
        DataTable.Add(new FishingUpgradeUnit());

        string[] priceArray = dataArr[1].Split(",");
        for(int i = 0; i < priceArray.Length; i++){
            DataTable[Size].Price.Add(float.Parse(priceArray[i]));
        }

        string[] valueArray = dataArr[2].Split(",");    
        for(int i = 0; i < valueArray.Length; i++){
            DataTable[Size].Value.Add(float.Parse(valueArray[i]));
        }

        DataTable[Size].MaxLevel = int.Parse(dataArr[3]);

        ++Size;
    }
}
