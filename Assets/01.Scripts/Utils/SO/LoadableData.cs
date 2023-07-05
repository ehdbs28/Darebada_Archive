using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataTable<T> where T : class{
    [SerializeField]
    private List<T> _data = new List<T>();

    public T this[int idx]{
        get{
            if(idx < 0 || idx >= _data.Count)
                return null;

            return _data[idx];
        }
    }

    public void Add(T value){
        _data.Add(value);
    }

    public void Clear(){
        _data.Clear();
    }
}

public abstract class LoadableData : ScriptableObject
{
    public DataLoadType Type;
    public abstract void SetUp(string[] dataArr);
    public abstract void Clear();
}
