using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUnit
{
    public SaveData SaveData { get; set; }

    public string SAVE_PATH { get; private set; }

    public DataUnit(string FILE_PATH, string name){
        SAVE_PATH = $"{FILE_PATH}/{name}.json";
    }
}
