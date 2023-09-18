using UnityEngine;

public class AquariumSaveData : SaveData
{
    private string _key;
    private GameObject _initObj;

    public AquariumSaveData(string key, GameObject initObj, string FILE_PATH, string name) : base(FILE_PATH, name)
    {
        _key = key;
        _initObj = initObj;
        _savePath = $"{FILE_PATH}/{name}";
    }

    public void SaveObj(GameObject obj)
    {
        ES3.Save<GameObject>(_key, obj);
    }

    public GameObject LoadObj()
    {
        if (ES3.KeyExists(_key))
        {
            return ES3.Load<GameObject>(_key);
        }
        else
        {
            return _initObj;
        }
    }

    protected override void LoadData(string json)
    {
    }

    public override void Reset()
    {
        ES3.DeleteKey(_key);
    }
}
