using System.Collections.Generic;
using Core;

[System.Serializable]
public class GameData : SaveData
{
    public float GameTime = 0.0f;
    public int TotalDay = 0;
    public GameDate GameDateTime = default(GameDate);
    public int HoldingGold = 0;
    public List<LetterUnit> HoldingLetter = null;

    public override void Reset()
    {
        GameTime = 0.0f;
        TotalDay = 0;
        GameDateTime = default(GameDate); 
        HoldingGold = 0;
        HoldingLetter = null;
    }
}
