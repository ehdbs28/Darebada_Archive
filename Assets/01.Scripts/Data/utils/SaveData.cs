using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class SaveData
{
    protected string _savePath;

    public SaveData(string FILE_PATH, string name){
        _savePath = $"{FILE_PATH}/{name}.json";
    }

    public virtual void Load(){
        if(File.Exists(_savePath)){
            string data = File.ReadAllText(_savePath);
            LoadData(data);
        }
        else{
            Reset();
        }
    }

    public virtual void Save(string data = ""){
        File.WriteAllText(_savePath, data);
    }

    protected abstract void LoadData(string json = "");
    public abstract void Reset();
}
