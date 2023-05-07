using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BaitData")]
public class BaitSO : MonoBehaviour
{
    public BIOME VaildBiome;                //��ȿ ���̿�
    public float VaildBiomeIncreaseValue;   //��ȿ ���̿� ���� �� ������
    public float VaildMinTime;              //��ȿ �ּ� �ð���
    public float VaildMaxTime;              //��ȿ �ִ� �ð���
    public float VaildTimeIncreaseValue;    //��ȿ �ð��� ���� �� ������
    public float VaildMinDepth;             //��ȿ ���� �ּҰ�
    public float VaildMaxDepth;             //��ȿ ���� �ִ밪
    public float VaildDepthIncreaseValue;   //��ȿ ���� ���� �� ������
    public float VaildRarity;               //��ȿ ��͵�
    public float VaildRarityIncreaseValue;  //��ȿ ��͵� ���� �� ������
}
