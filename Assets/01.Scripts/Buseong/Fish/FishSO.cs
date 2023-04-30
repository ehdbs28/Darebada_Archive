using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/FishData")]
public class FishSO : ScriptableObject
{
    public BIOME HabitatBiome;      //서식 바이옴
    public float HabitatX;          //서식X값
    public float HabitatY;          //서식y값
    public FISHSPECIES FishSpecies; //어종
    public Vector3 Direction;       //방향
    public float SwimSpeed;         //헤엄 속도
    public float TurnSpeed;         //방향 전환 속도
    public float Rarity;            //희귀도
    public float Level;             //레벨
    public float Cost;              //가격
    public float Size;              //사이즈
    public float SpawnPercent;      //스폰 확률

    public Material TestMat;        //테스트용
    [SerializeField]
    public GameObject _fishPrefab;
}
