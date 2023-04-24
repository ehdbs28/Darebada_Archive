using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class StatePanel : MonoBehaviour
{
    public GameObject upgradeObj;
    Facility _script;
    private void OnEnable()
    {
        _script = upgradeObj.GetComponent<Facility>();
    }
    public void UpgradeLevel()
    {
        //물고기 최대량 추가
        if(_script.GetComponent<Fishbowl>())
        {
            _script.GetComponent<Fishbowl>().Upgrade();
        }
    }
    public void AddFish()
    {
        //물고기 추가
    }
    public void AddDeco()
    {
        //장식 추가
    }
}
