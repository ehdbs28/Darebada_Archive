using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataTable<T> where T : class{
    [SerializeField]
    private List<T> Datas = new List<T>();

    public T this[int idx]{
        get{
            if(idx < 0 || idx >= Datas.Count)
                return null;

            return Datas[idx];
        }
    }

    public void Add(T value){
        Datas.Add(value);
    }
}

public abstract class LoadableData : ScriptableObject
{
    public abstract void SetUp(string[] dataArr);
}
