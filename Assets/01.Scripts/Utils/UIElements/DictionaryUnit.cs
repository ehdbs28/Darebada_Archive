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

    public bool IsUnknown { get; private set; } = false;

    public DictionaryUnit(VisualElement root, VisualTreeAsset rarityStar, FishDataUnit dataUnit){
        _root = root;

        var fishImage = _root.Q("fish-image");
        fishImage.style.backgroundImage = new StyleBackground(dataUnit.Visual.Profile);

        var starContainer = _root.Q("star-container");
        for (int i = 0; i < dataUnit.Rarity; i++)
        {
            var star = rarityStar.Instantiate().Q("star");
            starContainer.Add(star);
        }
        
        DictionaryData data = GameManager.Instance.GetManager<DataManager>().GetData(DataType.DictionaryData) as DictionaryData;
        _dataUnit = data.Units.List.Find(unit => unit.Name == dataUnit.Name);

        IsUnknown = _dataUnit == null;
        
        if(IsUnknown){
            root.AddToClassList("unknown");
        }
        else{
            root.RemoveFromClassList("unknown");
        }
    }
}
    