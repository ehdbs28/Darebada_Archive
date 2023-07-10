using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DictionaryUnit
{
    private VisualElement _root;
    public VisualElement Root => _root;

    private DictionaryDataUnit _dataUnit;
    public DictionaryDataUnit DataUnit => _dataUnit;

    public bool IsUnknown { get; set; }

    public DictionaryUnit(VisualElement root){
        _root = root;
        DictionaryData data = GameManager.Instance.GetManager<DataManager>().GetData(DataType.DictionaryData) as DictionaryData;
        _dataUnit = data.Units.Find(unit => unit.Name == root.name);
        IsUnknown = _dataUnit == null;
        if(IsUnknown){
            root.AddToClassList("unknown");
        }
        else{
            root.RemoveFromClassList("unknown");
        }
    }
}
