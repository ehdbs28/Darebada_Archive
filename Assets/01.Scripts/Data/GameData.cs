using System.Collections.Generic;
using UnityEngine;
using Core;

[System.Serializable]
public class GameData : SaveData
{
    public float GameTime = 0.0f;
    public int TotalDay = 0;
    public GameDate GameDateTime = null;
    public int HoldingGold = 0;
    public SerializeList<LetterUnit> HoldingLetter = new SerializeList<LetterUnit>();

    public GameData(string FILE_PATH, string name) : base(FILE_PATH, name)
    {
        Reset();
    }

    protected override void LoadData(string json)
    {
        GameData data = JsonUtility.FromJson<GameData>(json);
        GameTime = data.GameTime;
        TotalDay = data.TotalDay;
        GameDateTime = data.GameDateTime;
        HoldingGold = data.HoldingGold;
        HoldingLetter = data.HoldingLetter;
    }

    protected override void Reset()
    {
        GameTime = 0.0f;
        TotalDay = 0;
        GameDateTime = new GameDate(0, 3, 0);
        HoldingGold = 0;
        HoldingLetter = new SerializeList<LetterUnit>();
    }
}
