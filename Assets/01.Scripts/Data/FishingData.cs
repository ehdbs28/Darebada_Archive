using UnityEngine;

[System.Serializable]
public class FishingData : SaveData
{
    public int ThrowPower_Level = 1;
    public int StringLength_Level = 1;
    public int StringStrength_Level = 1;

    public int[] ItemVal;

    public FishingData(string FILE_PATH, string name) : base(FILE_PATH, name)
    {
    }

    protected override void LoadData(string json)
    {
        FishingData data = JsonUtility.FromJson<FishingData>(json);
        ThrowPower_Level = data.ThrowPower_Level;
        StringLength_Level = data.StringLength_Level;
        StringStrength_Level = data.StringStrength_Level;
        ItemVal = data.ItemVal;
    }

    protected override void Reset()
    {
        ThrowPower_Level = 1;
        StringLength_Level = 1;
        StringStrength_Level = 1;
        ItemVal = new int[(int)FishingItemType.Count];
    }
}