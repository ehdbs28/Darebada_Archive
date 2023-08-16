using Core;
using UnityEngine;

public class InventoryData : SaveData
{
    public SerializeList<InventoryUnit> Units;

    public InventoryData(string FILE_PATH, string name) : base(FILE_PATH, name)
    {
    }

    protected override void LoadData(string json)
    {
        InventoryData data = JsonUtility.FromJson<InventoryData>(json);
        Units = data.Units;
    }

    protected override void Reset()
    {
        Units = new SerializeList<InventoryUnit>();
    }
}
