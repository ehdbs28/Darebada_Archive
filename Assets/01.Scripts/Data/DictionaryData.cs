using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DictionaryDataUnit{
    public string Name;
    public Sprite Image;
    public string Desc;
    public OceneType Habitat;
    public List<string> Favorites;
    public float MaxLenght;
    public float MaxWeight;
    public bool IsDotated;
}

[System.Serializable]
public class DictionaryData : SaveData
{
    public List<DictionaryDataUnit> Units;

    public DictionaryData(string FILE_PATH, string name) : base(FILE_PATH, name)
    {
    }

    protected override void LoadData(string json)
    {
        DictionaryData data = JsonUtility.FromJson<DictionaryData>(json);
        Units = data.Units;
    }

    protected override void Reset()
    {
        Units = new List<DictionaryDataUnit>();
    }
}
