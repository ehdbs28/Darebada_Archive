using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/FishData")]
public class FishSO : LoadableData
{
    //public BIOME HabitatBiome;      //서식 바이옴
    //public float HabitatX;          //서식X값
    //public float HabitatY;          //서식y값
    //public FISHSPECIES FishSpecies; //어종
    //public float SwimSpeed;         //헤엄 속도
    //public float TurnSpeed;         //방향 전환 속도
    //public float Rarity;            //희귀도
    //public float Level;             //레벨
    //public float Cost;              //가격
    //public float Size;              //사이즈
    //public float SpawnPercent;      //스폰 확률
    //public bool IsFlock;            //뭉쳐다니는지 아닌지

    //public Material TestMat;        //테스트용
    //[SerializeField]
    //public GameObject _fishPrefab;

    //수정
    public BIOME HabitatBiome;
    public float Speed;
    public string FishSpecie;
    public int Rarity;
    public Mesh Mesh;
    public float SpawnPercentage;

    public override void SetUp(string[] dataArr)
    {
        //HabitatBiome = (BIOME)int.Parse(dataArr[1]);
        //HabitatX = float.Parse(dataArr[2]);
        //HabitatY = float.Parse(dataArr[3]);
        //FishSpecies = (FISHSPECIES)int.Parse(dataArr[4]);
        //SwimSpeed = float.Parse(dataArr[5]);
        //TurnSpeed = float.Parse(dataArr[6]);
        //Rarity = float.Parse(dataArr[7]);
        //Level = float.Parse(dataArr[8]);
        //Cost = float.Parse(dataArr[9]);
        //Size = float.Parse(dataArr[10]);
        //SpawnPercent = float.Parse(dataArr[11]);
    }
}
