using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeData : SaveData
{
    public List<bool> Biomes;

    public BiomeData(string FILE_PATH, string name) : base(FILE_PATH, name)
    {
    }

    protected override void LoadData(string json)
    {
        BiomeData data = JsonUtility.FromJson<BiomeData>(json);
        Biomes = data.Biomes;
    }

    public override void Reset()
    {
        Biomes = new List<bool>();
        for(int i = 0; i < (int)OceanType.Count; i++){
            Biomes.Add(false);
        }
        Biomes[0] = true;
    }
}
