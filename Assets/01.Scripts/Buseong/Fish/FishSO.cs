using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/FishData")]
public class FishSO : LoadableData
{
    //public BIOME HabitatBiome;      //���� ���̿�
    //public float HabitatX;          //����X��
    //public float HabitatY;          //����y��
    //public FISHSPECIES FishSpecie; //����
    //public float Speed;         //��� �ӵ�
    //public float TurnSpeed;         //���� ��ȯ �ӵ�
    //public float Rarity;            //��͵�
    //public float Level;             //����
    //public float Cost;              //����
    //public float Size;              //������
    //public float SpawnPercentage;      //���� Ȯ��
    //public bool IsFlock;            //���Ĵٴϴ��� �ƴ���
    //public Mesh Mesh;

    //public Material TestMat;        //�׽�Ʈ��
    //[SerializeField]
    //public GameObject _fishPrefab;

    public string Name;
    public int Price;
    public OceneType Habitat;
    public int Rarity;              
    public int Speed;               
    public float MinLength;
    public float MaxLength;
    public float MinWeight;
    public float MaxWeight;
    public float SpawnPercentage;
    public Mesh Mesh;

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

    public override void AddData(string[] dataArr)
    {
        //HabitatBiome = (BIOME)int.Parse(dataArr[1]);
        //HabitatX = float.Parse(dataArr[2]);
        //HabitatY = float.Parse(dataArr[3]);
        //FishSpecie = (FISHSPECIES)int.Parse(dataArr[4]);
        //Speed = float.Parse(dataArr[5]);
        //TurnSpeed = float.Parse(dataArr[6]);
        //Rarity = float.Parse(dataArr[7]);
        //Level = float.Parse(dataArr[8]);
        //Cost = float.Parse(dataArr[9]);
        //Size = float.Parse(dataArr[10]);
        //SpawnPercentage = float.Parse(dataArr[11]);

        Name = dataArr[0];
        Price = int.Parse(dataArr[1]);
        Habitat = (OceneType)int.Parse(dataArr[2]);
        Rarity = int.Parse(dataArr[3]);
        Speed = int.Parse(dataArr[4]);
        MinLength = float.Parse(dataArr[5]);
        MaxLength = float.Parse(dataArr[6]);
        MinWeight = float.Parse(dataArr[7]);
        MaxWeight = float.Parse(dataArr[8]);
        SpawnPercentage = float.Parse(dataArr[9]);
    }
}
