using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

[System.Serializable]
public class ChallengeSaveData
{
    public int totalValue;
    public bool isClear;
    public bool isReceipt;
}

public class ChallengeData : SaveData
{
    public ChallengeSaveData[] units;

    public ChallengeData(string FILE_PATH, string name) : base(FILE_PATH, name)
    {
    }

    protected override void LoadData(string json)
    {
        var data = JsonUtility.FromJson<ChallengeData>(json);
        units = data.units;
    }

    protected override void Reset()
    {
        units = new ChallengeSaveData[7];
        for(int i = 0; i < 7; i++)
        {
            units[i] = new ChallengeSaveData();
        }
    }
}
