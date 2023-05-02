using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance = null;
    // 나중에 게임 매니저에서 관리

    private Dictionary<DataType, DataUnit> _dataUnits;

    private string DATA_PATH = "";

    private float _autoSaveTime = 3f;

    private void Awake() {
        if(Instance == null){
            Instance = this;
        }

        _dataUnits = new Dictionary<DataType, DataUnit>();

        // 모바일 빌드 시에는 바꿔야 해
        // DATA_PATH = Application.persistentDataPath + "/Save";
        DATA_PATH = Application.dataPath + "/Save";

        if(!Directory.Exists(DATA_PATH))
            Directory.CreateDirectory(DATA_PATH);

        foreach(var type in Enum.GetValues(typeof(DataType))){
            _dataUnits.Add( (DataType)type, new DataUnit(DATA_PATH, type.ToString()));
        }

        LoadData();

        InvokeRepeating("SaveDataAll", 0.5f, _autoSaveTime);
    }

    private void LoadData(){
        foreach(var data in _dataUnits){
            if(File.Exists(data.Value.SAVE_PATH)){
                string stringData = File.ReadAllText(data.Value.SAVE_PATH);
                if(data.Key == Core.DataType.BoatData){
                    data.Value.SaveData = JsonUtility.FromJson<BoatData>(stringData);
                }
                else if(data.Key == Core.DataType.FishingData){
                    data.Value.SaveData = JsonUtility.FromJson<FishingData>(stringData);
                }
            }
            else{
                if(data.Key == Core.DataType.BoatData){
                    data.Value.SaveData = new BoatData();
                }
                else if(data.Key == Core.DataType.FishingData){
                    data.Value.SaveData = new FishingData();
                }
                SaveData(data.Value);
            }
        }
    }

    private void SaveData(DataUnit data){
        string stringData = JsonUtility.ToJson(data.SaveData);
        File.WriteAllText(data.SAVE_PATH, stringData);
    }

    private void SaveDataAll(){
        foreach(var data in _dataUnits.Values){
            SaveData(data);
        }
    }

    public SaveData GetData(DataType type){
        SaveData returnData = null;
        
        foreach(var data in _dataUnits.Where(data => data.Key == type)){
            returnData = data.Value.SaveData;
        }

        return returnData;
    }
}
