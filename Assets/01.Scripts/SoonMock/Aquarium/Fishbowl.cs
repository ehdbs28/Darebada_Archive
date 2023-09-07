using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEditor.AddressableAssets.Build.AnalyzeRules;
using UnityEngine;
using UnityEngine.UIElements;


public class Fishbowl :  Facility
{
    public Dictionary <string, AquariumBoids> boids = new Dictionary<string, AquariumBoids>();
    public List<GameObject> boidObjects = new List<GameObject>();
    public List<Transform> decoTrs= new List<Transform>();
    public GameObject boidObject;
    public GameObject decoObject;
    public DecoController decoController;
    public int MaxDecoCount
    {
        get { return level * 3; }
        private set { }
    }
    public int MaxFishCount
    {
        get { return level * 3; }
        private set { }
    }
    public int level = 1;
    public bool CheckRemainedFishs()
    {
        int curCnt = 0;
        foreach (var boid in boids)
        {
            curCnt += boid.Value.Fishes.Count;
        }
        Debug.Log(MaxDecoCount - curCnt); ;
        if (MaxFishCount - curCnt >0)
        {
            return true;
        }
        return false;
    }
    public void AddDeco(int idx)
    {
        if (!decoController)
        {
            GameObject decoConObj = Instantiate(new GameObject());
            decoController = decoConObj.AddComponent<DecoController>();
            decoConObj.transform.SetParent(transform, false);
            decoController.decoObject = decoObject.GetComponent<Deco>();
            decoController.decoPositions = decoTrs;
        }

        if (MaxDecoCount - decoController.decos.Count > 0)
        {
            Debug.Log(MaxDecoCount - decoController.decos.Count);
            decoController.AddDeco( AquariumManager.Instance.decoVisuals[idx]);
        }
    }
    public void AddFIsh(FishDataUnit fishData)
    {
        if(CheckRemainedFishs())
        {
            if(boids.ContainsKey(fishData.Name))
            {
                boids[fishData.Name].GenerateBoids();
            }else
            {
                GameObject tmp = new GameObject(fishData.Name);
                AquariumBoids boid = tmp.AddComponent<AquariumBoids>();
                tmp.transform.SetParent(transform, true);
                tmp.transform.localPosition = Vector3.up * 0.8f;
                boid.boidUnitPrefab = boidObject.GetComponent<AuariumBoidUnit>();
                boid.FishData = fishData;
                boids.Add(fishData.Name, boid);
                boidObjects.Add(tmp);
                boid.boundsWeight = 2;
                boid.obstacleWeight = 100;
                boid.egoWeight = 0.5f;
                boid.GenerateBoids();
            }

        }
    }
    public Fishbowl Upgrade()
    {
        Fishbowl newOne = Instantiate(AquariumManager.Instance.fishBowls[level], transform.position, Quaternion.identity).GetComponent<Fishbowl>();
        newOne.boids = boids;
        newOne.level = level+1;
        newOne.boidObjects = boidObjects;
        newOne.decoController= decoController;
        foreach(GameObject obj in  boidObjects)
        {
            Debug.Log(obj.name);
            obj.transform.parent = newOne.transform;
            obj.GetComponent<AquariumBoids>().SetPosZero();
            obj.GetComponent<AquariumBoids>().SetMove(false);
        }
        decoController.decoPositions = newOne.decoTrs;
        decoController.gameObject.transform.SetParent(newOne.transform); 
        Destroy(gameObject);
        return newOne;

    }
    private void Awake()
    {
        
        _collider = GetComponent<Collider>();
    }

    public override Facility OnTouched()
    {
        return this;
    }
}
