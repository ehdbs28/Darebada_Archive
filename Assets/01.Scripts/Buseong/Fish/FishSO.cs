using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Biome
{
    TUNA,
    BLOWFISH,
    SHARK,
    MACKEREL
}

public enum FishSpecies
{
    TEST1,
    TEST2,
    TEST3
}

[CreateAssetMenu(menuName = "SO/FishData")]
public class FishSO : ScriptableObject
{
    public Biome HabitatBiome;      //서식 바이옴
    public float HabitatX;          //서식X값
    public float HabitatY;          //서식y값
    public FishSpecies FishSpecies; //어종
    public Vector3 Direction;       //방향
    public float SwimSpeed;         //헤엄 속도
    public float TurnSpeed;         //방향 전환 속도
    public float Rarity;            //희귀도
    public float Level;             //레벨
    public float cost;              //가격
    public float size;              //사이즈
}
