using System.Collections.Generic;
using UnityEngine;
using Core;

[System.Serializable]
public class GameData : SaveData
{
    public float GameTime = 0.0f;
    public int TotalDay = 1;
    public GameDate GameDateTime = null;
    public int HoldingGold = 0;
    public SerializeList<LetterUnit> HoldingLetter = new SerializeList<LetterUnit>();
    public bool CampTutorial = false;
    public bool OceanTutorial = false;
    public bool AquariumTutorial = false;

    public GameData(string FILE_PATH, string name) : base(FILE_PATH, name)
    {
    }

    protected override void LoadData(string json)
    {
        GameData data = JsonUtility.FromJson<GameData>(json);
        GameTime = data.GameTime;
        TotalDay = data.TotalDay;
        GameDateTime = data.GameDateTime;
        HoldingGold = data.HoldingGold;
        HoldingLetter = data.HoldingLetter;
        CampTutorial = data.OceanTutorial;
        OceanTutorial = data.OceanTutorial;
        AquariumTutorial = data.AquariumTutorial;
    }

    public override void Reset()
    {
        GameTime = 0.0f;
        TotalDay = 1;
        GameDateTime = new GameDate(0, 3, 0);
        HoldingGold = 0;
        HoldingLetter = new SerializeList<LetterUnit>();
        CampTutorial = false;
        OceanTutorial = false;
        AquariumTutorial = false;
    }
}
