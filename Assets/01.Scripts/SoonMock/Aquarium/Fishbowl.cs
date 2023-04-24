using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishbowl :  Facility
{
    public int MaxAmount = 3;
    
    Dictionary<int, Fish> fishs = new Dictionary<int, Fish>();
    [SerializeField] int tempKey, tempAmount;
    public void AddFish(int key, int amount)
    {        
        if(fishs.ContainsKey(key))
        {
            fishs[key].amount = amount;
        }else
        {
            fishs.Add(key, new Fish());
            fishs[key].amount = amount;
            fishs[key].id = key;
            fishs[key].ChangeName();
        }
        
    }

    public void Upgrade()
    {
        if(AquariumManager.Instance.Gold >= 3*MaxAmount * 300)
        {
            MaxAmount++;
            AquariumManager.Instance.Gold -= 3 * MaxAmount * 300;
        }
    }
    public override Facility OnTouched()
    {
        return this;
    }
}
