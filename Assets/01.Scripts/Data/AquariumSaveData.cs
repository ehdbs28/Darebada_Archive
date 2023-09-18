using UnityEngine;

public class AquariumSaveData : SaveData
{
    public GameObject AquariumObject = null;
    
    private string _key;
    private GameObject _initObj;

    public AquariumSaveData(string key, GameObject initObj, string FILE_PATH, string name) : base(FILE_PATH, name)
    {
        _key = key;
        _initObj = initObj;
        _savePath = $"{FILE_PATH}/{name}";
    }

    public override void Save(string data = "")
    {
        ES3.Save<GameObject>(_key, AquariumObject);
    }

    public override void Load()
    {
        if (ES3.KeyExists(_key))
        {
            AquariumObject = ES3.Load<GameObject>(_key);
        }
        else
        {
            Reset();
        }
    }

    protected override void LoadData(string json)
    {
        
    }

    public override void Reset()
    {
        AquariumObject = _initObj;
    }
}
