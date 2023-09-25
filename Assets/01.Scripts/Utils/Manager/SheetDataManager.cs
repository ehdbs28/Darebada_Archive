using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetDataManager : MonoBehaviour, IManager
{
    [SerializeField]
    private List<LoadableData> _dataTables = new List<LoadableData>();
    private readonly Dictionary<DataLoadType, LoadableData> _datas = new Dictionary<DataLoadType, LoadableData>();

    public void InitManager()
    {
        for(int i = 0; i < _dataTables.Count; i++){
            _datas.Add(_dataTables[i].Type, _dataTables[i]);
        }
    }

    public LoadableData GetData(DataLoadType type){
        return _datas[type];
    }

    public void ResetManager(){}
    public void UpdateManager(){}
}
