using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class AchievementSO : ScriptableObject
{
    public string achievenemtName;
    public int id;
    public List<FishInfoSO> completedList;
    public List<FishInfoSO> infoList;
    public bool isCompleted = false;
    public bool isRewardTaked = false;
    public Sprite rewardImage;
    public string rewardText;
    public int rewardAmount;

}
