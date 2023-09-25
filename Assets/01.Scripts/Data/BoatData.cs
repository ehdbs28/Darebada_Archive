using System.IO;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoatData : SaveData
{
    public BoatDataUnit CurrentBoat = null;
    public int[] BoatPurchaseDetail;

    public float Fuel = 0;

    public BoatData(string FILE_PATH, string name) : base(FILE_PATH, name)
    {
    }

    protected override void LoadData(string json)
    {
        BoatData data = JsonUtility.FromJson<BoatData>(json);
        CurrentBoat = data.CurrentBoat;
        BoatPurchaseDetail = data.BoatPurchaseDetail;
        Fuel = data.Fuel;
    }

    public override void Reset()
    {
        CurrentBoat = null;
        BoatPurchaseDetail = new int[5] { 
            (int)BoatBuyState.Equip,
            (int)BoatBuyState.Sale,
            (int)BoatBuyState.Sale,
            (int)BoatBuyState.Sale,
            (int)BoatBuyState.Sale,    
        };
        Fuel = 0;
    }
}
