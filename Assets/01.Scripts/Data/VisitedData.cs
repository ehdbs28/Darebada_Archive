using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitedData : SaveData
{
    public int GameVisitedCount = 0;
    public int CampVisitedCount = 0;
    public int OceanVisitedCount = 0;
    public int AquariumVisitedCount = 0;
    public VisitedData(string FILE_PATH, string name) : base(FILE_PATH, name)
    {
    }

    public override void Reset()
    {
        CampVisitedCount = 0;
        OceanVisitedCount = 0;
        AquariumVisitedCount = 0;
    }

    protected override void LoadData(string json = "")
    {
        if(json!="")
        {
            VisitedData data = JsonUtility.FromJson<VisitedData>(json);
            CampVisitedCount = data.CampVisitedCount;
            OceanVisitedCount = data.OceanVisitedCount;
            OceanVisitedCount= data.AquariumVisitedCount;
        }
    }
}
