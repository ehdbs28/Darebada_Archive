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

public abstract class LoadableData<T> : ScriptableObject where T : class
{
    public DataLoadType Type;
    public int Size = 0;
    public DataTable<T> DataTable = new DataTable<T>();
    public abstract void AddData(string[] dataArr);
    public void Clear(){
        Size = 0;
        DataTable.Clear();
    }
}
