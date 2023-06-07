using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/FishData")]
public class FishSO : LoadableData
{
    public float Rarity;            //Èñ±Íµµ

    public GameObject _fishPrefab;

    public override void SetUp(string[] dataArr)
    {
        Rarity = float.Parse(dataArr[0]);
    }
}
