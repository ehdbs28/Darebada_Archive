using System.IO;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoatData : SaveData
{
    //public BoatStatSO BoatStat = null;
    public float Fuel = 0;

    public BoatData(string FILE_PATH, string name) : base(FILE_PATH, name)
    {
    }

    protected override void LoadData(string json)
    {
        BoatData data = JsonUtility.FromJson<BoatData>(json);
        Fuel = data.Fuel;
    }

    protected override void Reset()
    {
        //BoatStat = null;
        Fuel = 0;
    }
}
