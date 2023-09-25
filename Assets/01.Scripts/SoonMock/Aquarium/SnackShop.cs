using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnackShop : Facility
{
    public int cost;
    public int level = 1;
    public int amount;
    
    private void OnEnable()
    {
        cost = level * 900;
        amount = level * 120;
    }
    public void Upgrade()
    {
        cost = level * 900;
        //if(AquariumManager.Instance.Gold >= cost)
        //{
        //    AquariumManager.Instance.Gold -= cost;
        //    level++;
        //    amount = level * 120;
        //}
        cost = level * 900;
    }
}
