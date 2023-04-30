using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/FishData")]
public class FishSO : ScriptableObject
{
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
    public float SpawnPercent;      //���� Ȯ��

    public Material TestMat;        //�׽�Ʈ��
    [SerializeField]
    public GameObject _fishPrefab;
}
