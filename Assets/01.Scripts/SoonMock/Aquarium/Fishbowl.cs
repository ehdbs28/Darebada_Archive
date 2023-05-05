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
    public List<SoonMockFish> fishList;
}
public class Fishbowl :  Facility
{
    public int Cost;
    public int MaxAmount = 3;
    
    Dictionary<int, Fishs> fishs = new Dictionary<int, Fishs>();
    [SerializeField]Dictionary<int,Deco> Decorations = new Dictionary<int, Deco>();
    [SerializeField] int tempKey, tempAmount;
    public void AddFish(int key, int amount)
    {        
        if(fishs.ContainsKey(key))
        {
            Debug.Log(fishs[key].fishList.Count);
            for(int i = 0; i < amount - fishs[key].fishList.Count;i++)
            {
                GameObject obj = AquariumManager.Instance.AddFish(key, transform);
                
                obj.GetComponent<SoonMockFish>().id = key;
                obj.GetComponent<SoonMockFish>().ChangeName();
                fishs[key].fishList.Add(obj.GetComponent<SoonMockFish>());
            }
        }else
        {
            GameObject obj = AquariumManager.Instance.AddFish(key, transform);
            fishs.Add(key, new Fishs());
            fishs[key].fishList = new List<SoonMockFish>();
            fishs[key].fishList.Add(obj.GetComponent<SoonMockFish>());
            obj.GetComponent<SoonMockFish>().id = key;
            obj.GetComponent<SoonMockFish>().ChangeName();
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
    public void AddDeco(int id)
    {
        if(!Decorations.ContainsKey(id))
        {
            Deco deco = AquariumManager.Instance.AddDeco(id, transform).GetComponent<Deco>();
            Decorations.Add(id, deco);
            deco.gameObject.transform.localPosition = deco.pos;
            AquariumManager.Instance.decoCount++;
        }
        else
        {
            Deco deco = Decorations[id];
            Decorations.Remove(id);
            Destroy(deco.gameObject);
            AquariumManager.Instance.decoCount--;
        }
    }
    public override Facility OnTouched()
    {
        return this;
    }
}
