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
        //����� �ִ뷮 �߰�
        if(_script.GetComponent<Fishbowl>())
        {
            _script.GetComponent<Fishbowl>().Upgrade();
        }
    }
    public void AddFish()
    {
        //����� �߰�
    }
    public void AddDeco()
    {
        //��� �߰�
    }
}
