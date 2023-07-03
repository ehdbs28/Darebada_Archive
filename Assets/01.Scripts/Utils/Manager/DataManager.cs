using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : IManager
{
    private Dictionary<DataType, SaveData> _dataUnits;

    private string DATA_PATH = "";

    public DataManager(){
        ResetManager();
    }

    public void InitManager() {
        _dataUnits = new Dictionary<DataType, SaveData>();

        // 모바일 빌드 시에는 바꿔야 해
        // DATA_PATH = Application.persistentDataPath + "/Save";
        DATA_PATH = Application.dataPath + "/Save";

        if(!Directory.Exists(DATA_PATH))
            Directory.CreateDirectory(DATA_PATH);

        _dataUnits.Add(DataType.BoatData, new BoatData(DATA_PATH, "BoatData"));
        _dataUnits.Add(DataType.FishingData, new BoatData(DATA_PATH, "FishingData"));
        _dataUnits.Add(DataType.GameData, new BoatData(DATA_PATH, "GameData"));

        LoadData();
    }

    private void LoadData(){
        foreach(var data in _dataUnits.Values){
            data.Load();
        }
    }

    private void SaveData(SaveData data){
        string stringData = JsonUtility.ToJson(data);
        data.Save(stringData);
    }

    private void SaveDataAll(){
        foreach(var data in _dataUnits.Values){
            SaveData(data);
        }
    }

    public SaveData GetData(DataType type){
        return _dataUnits[type];
    }

    public void ResetManager(){}
    public void UpdateManager(){}
}
