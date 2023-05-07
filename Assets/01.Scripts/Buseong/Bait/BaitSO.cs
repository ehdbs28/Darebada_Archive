using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BaitData")]
public class BaitSO : MonoBehaviour
{
    public BIOME VaildBiome;                //유효 바이옴
    public float VaildBiomeIncreaseValue;   //유효 바이옴 맞춤 시 증가값
    public float VaildMinTime;              //유효 최소 시간대
    public float VaildMaxTime;              //유효 최대 시간대
    public float VaildTimeIncreaseValue;    //유효 시간대 맞춤 시 증가값
    public float VaildMinDepth;             //유효 깊이 최소값
    public float VaildMaxDepth;             //유효 깊이 최대값
    public float VaildDepthIncreaseValue;   //유효 깊이 맞춤 시 증가값
    public float VaildRarity;               //유효 희귀도
    public float VaildRarityIncreaseValue;  //유효 희귀도 맞춤 시 증가값
}
