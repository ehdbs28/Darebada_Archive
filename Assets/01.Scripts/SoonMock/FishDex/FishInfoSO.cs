using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RARITY
{
    S,
    A,
    B,
    C,
    D
}
[CreateAssetMenu]
public class FishInfoSO : ScriptableObject
{
    public string       fishName;
    public string       orderAndGenus;
    public Sprite       icon;
    public Sprite       obscuredIcon;
    public bool         hadDonated;
    public string       features;
    public float        biggestSize;
    public float        biggestWeight;
    public string       habitat;
    public RARITY       rarity;
    public string    necessity;//필요한 장식 ID
}
