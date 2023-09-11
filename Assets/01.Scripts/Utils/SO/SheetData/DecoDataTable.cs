using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[SerializeField]
public class DecoDataUnit
{
    public string Name;
}
public class DecoDataTable : LoadableData
{
    public DataTable<DecoDataUnit> DataTable = new DataTable<DecoDataUnit>();

    private OceanType _prevOceanType = OceanType.RichOcean;
    private int _localFishCnt = 0;

    public override void AddData(string[] dataArr)
    {
        DataTable.Add(new FishDataUnit());

        DataTable[Size].Name = dataArr[0];
        if (_prevOceanType != DataTable[Size].Habitat)
        {
            _localFishCnt = 0;
            _prevOceanType = DataTable[Size].Habitat;
        }

#if UNITY_EDITOR
        string path = $"Assets/06.SO/DecoVisual/{(int)DataTable[Size].Habitat:D2}.{DataTable[Size].Habitat}/{_localFishCnt:D2}.{dataArr[1]}.asset";
        DataTable[Size].Visual = AssetDatabase.LoadAssetAtPath<FishVisual>(path);
#endif

        string[] favorites = dataArr[13].Split(",");
        DataTable[Size].Favorites = new List<string>();
        foreach (var f in favorites)
        {
            DataTable[Size].Favorites.Add(f);
        }

        DataTable[Size].Description = dataArr[14];

        ++Size;
        ++_localFishCnt;
    }

    public override void Clear()
    {
        base.Clear();
        DataTable.Clear();
    }
}
