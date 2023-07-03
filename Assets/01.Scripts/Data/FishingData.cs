[System.Serializable]
public class FishingData : SaveData
{
    public int ThrowPower_Lever = 1;
    public int StringLength_Lever = 1;
    public int StringStrength_Level = 1;

    public override void Reset()
    {
        ThrowPower_Lever = 1;
        StringLength_Lever = 1;
        StringStrength_Level = 1;
    }
}