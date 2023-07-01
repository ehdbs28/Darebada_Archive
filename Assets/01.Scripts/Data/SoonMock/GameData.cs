using System.Collections.Generic;

[System.Serializable]
public class GameData : SaveData
{
    public float GameTime = 0.0f;
    public int TotalDay = 0;
    //public GameDate GameDateTie = null;
    public int HoldingGold = 0;
    public List<LetterUnit>HoldingLetter = null;
}
