using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Fishs
{
    public List<Fish> fishList;
}
public class Fishbowl :  Facility
{
    public int Cost;
    public int MaxAmount = 3;
    
    Dictionary<int, Fishs> fishs = new Dictionary<int, Fishs>();
    [SerializeField]List<int> Decorations = new List<int>();
    [SerializeField] int tempKey, tempAmount;
    public void AddFish(int key, int amount)
    {        
        if(fishs.ContainsKey(key))
        {
            Debug.Log(fishs[key].fishList.Count);
            for(int i = 0; i < amount - fishs[key].fishList.Count;i++)
            {
                GameObject obj = AquariumManager.Instance.AddFish(key, transform);
                
                obj.GetComponent<Fish>().id = key;
                obj.GetComponent<Fish>().ChangeName();
                fishs[key].fishList.Add(obj.GetComponent<Fish>());
            }
        }else
        {
            GameObject obj = AquariumManager.Instance.AddFish(key, transform);
            fishs.Add(key, new Fishs());
            fishs[key].fishList = new List<Fish>();
            fishs[key].fishList.Add(obj.GetComponent<Fish>());
            obj.GetComponent<Fish>().id = key;
            obj.GetComponent<Fish>().ChangeName();
        }
        
    }
    public void OnEnable()
    {
        Cost = 3 * MaxAmount * 300;
    }

    public void Upgrade()
    {
        if (AquariumManager.Instance.Gold >= Cost)
        {
            MaxAmount++;
            AquariumManager.Instance.Gold -= Cost;
        }
        Cost = 3 * MaxAmount * 300;
    }
    public void AddDeco(int deco)
    {
        if(!Decorations.Equals(deco))
        {
            Decorations.Add(deco);
        }
    }
    public override Facility OnTouched()
    {
        return this;
    }
}
