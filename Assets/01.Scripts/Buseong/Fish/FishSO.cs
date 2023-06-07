using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/FishData")]
public class FishSO : LoadableData
{
    public float spawnHeight;          //����y��
    public float Rarity;            //��͵�

    public GameObject _fishPrefab;

    public override void SetUp(string[] dataArr)
    {
        spawnHeight = float.Parse(dataArr[0]);
        Rarity = float.Parse(dataArr[1]);
    }
}
