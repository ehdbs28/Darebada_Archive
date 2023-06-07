using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BIOME
{
    CLOUD,
    SOUTH,
    COLD,
    SILENCE
}

public enum FISHSPECIES
{
    TUNA,
    BLOWFISH,
    SHARK,
    MACKEREL
}

[CreateAssetMenu]
public class ItemSO : ScriptableObject // �κ��丮 SO
{
    public int width = 1;
    public int height = 1;

    public Sprite itemicon;

    //FishSO
    public BIOME HabitatBiome;      //���� ���̿�
    public float HabitatX;          //����X��
    public float HabitatY;          //����y��
    public FISHSPECIES FishSpecies; //����
    public Vector3 Direction;       //����
    public float SwimSpeed;         //��� �ӵ�
    public float TurnSpeed;         //���� ��ȯ �ӵ�
    public float Rarity;            //��͵�
    public float Level;             //����
    public float Cost;              //����
    public float Size;              //������
    public float SpawnPercent;
}
