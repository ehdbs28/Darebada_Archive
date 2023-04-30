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
    public Biome HabitatBiome;      //���� ���̿�
    public float HabitatX;          //����X��
    public float HabitatY;          //����y��
    public FishSpecies FishSpecies; //����
    public Vector3 Direction;       //����
    public float SwimSpeed;         //��� �ӵ�
    public float TurnSpeed;         //���� ��ȯ �ӵ�
    public float Rarity;            //��͵�
    public float Level;             //����
    public float cost;              //����
    public float size;              //������
}
