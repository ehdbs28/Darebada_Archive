using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/FishData")]
public class    FishSO : LoadableData
{
    public BIOME HabitatBiome;      //���� ���̿�
    public float HabitatX;          //����X��
    public float HabitatY;          //����y��
    public FISHSPECIES FishSpecies; //����
    public float SwimSpeed;         //��� �ӵ�
    public float TurnSpeed;         //���� ��ȯ �ӵ�
    public float Rarity;            //��͵�
    public float Level;             //����
    public float Cost;              //����
    public float Size;              //������
    public float SpawnPercent;      //���� Ȯ��
    public bool IsFlock;            //���Ĵٴϴ��� �ƴ���

    public Material TestMat;        //�׽�Ʈ��
    [SerializeField]
    public GameObject _fishPrefab;

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

    public override void AddData(string[] dataArr)
    {
        HabitatBiome = (BIOME)int.Parse(dataArr[1]);
        HabitatX = float.Parse(dataArr[2]);
        HabitatY = float.Parse(dataArr[3]);
        FishSpecies = (FISHSPECIES)int.Parse(dataArr[4]);
        SwimSpeed = float.Parse(dataArr[5]);
        TurnSpeed = float.Parse(dataArr[6]);
        Rarity = float.Parse(dataArr[7]);
        Level = float.Parse(dataArr[8]);
        Cost = float.Parse(dataArr[9]);
        Size = float.Parse(dataArr[10]);
        SpawnPercent = float.Parse(dataArr[11]);
    }
}
