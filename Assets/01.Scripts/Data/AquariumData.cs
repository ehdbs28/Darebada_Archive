using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class AquariumData : SaveData
{
    public int CleanScore = 0;
    public int PromotionPoint = 0;
    public int Decoration_Count = 0;
    public int Fishbowl_Count = 0;
    public int Shop_Count = 0;
    public int Road_Count = 0;
    public int CleanService_Amount = 0;

    public AquariumData(string FILE_PATH, string name) : base(FILE_PATH, name)
    {
    }

    public override void Reset()
    {
        CleanScore = 0;
        PromotionPoint = 0;
        Decoration_Count = 0;
        Fishbowl_Count = 0;
        Shop_Count = 0;
        Road_Count = 0;
        CleanService_Amount = 0;
    }

    protected override void LoadData(string json)
    {
        AquariumData loadedData = JsonUtility.FromJson<AquariumData>(json);
        CleanScore = loadedData.CleanScore;
        PromotionPoint= loadedData.PromotionPoint;
        Decoration_Count = loadedData.Decoration_Count;
        Fishbowl_Count = loadedData.Fishbowl_Count;
        Shop_Count= loadedData.Shop_Count;
        Road_Count  = loadedData.Road_Count;
        CleanService_Amount= loadedData.CleanService_Amount;
    }
}
