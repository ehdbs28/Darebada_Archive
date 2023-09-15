using System.Collections.Generic;
using UnityEngine;


public class Fishbowl :  Facility
{
    public Dictionary <string, AquariumBoids> boids = new Dictionary<string, AquariumBoids>();
    public List<GameObject> boidObjects = new List<GameObject>();
    public List<Transform> decoTrs= new List<Transform>();
    public List<AuariumBoidUnit> fishs = new List<AuariumBoidUnit>();
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
        Debug.Log(MaxFishCount - curCnt); ;
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
    
    public void RemoveDeco(int idx)
    {
        if (!decoController)
        {
            GameObject decoConObj = Instantiate(new GameObject());
            decoController = decoConObj.AddComponent<DecoController>();
            decoConObj.transform.SetParent(transform, false);
            decoController.decoObject = decoObject.GetComponent<Deco>();
            decoController.decoPositions = decoTrs;
        }
        string decoName = AquariumManager.Instance.decoVisuals[idx].Name;
        for(int i = 0; i <  decoController.decos.Count; i++)
        {
            if (decoController.decos[i].visualSO.Name == decoName)
            {
                decoController.RemoveDeco(i);
                i = -1;
            }
        }
    }
    
    public void AddFIsh(FishDataUnit fishData)
    {
        if(CheckRemainedFishs())
        {
            if(boids.ContainsKey(fishData.Name))
            {
                fishs.Add(boids[fishData.Name].GenerateBoids());
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
                fishs.Add( boid.GenerateBoids());

            }

        }
    }

    public void RemoveFish(int idx)
    {
        AquariumBoids selectedBoid =  fishs[idx].myBoids;
        GameObject obj = selectedBoid.Fishes[0].gameObject;
        selectedBoid.Fishes.RemoveAt(0);
        fishs.RemoveAt(idx);
        Destroy(obj);
        selectedBoid.FindNeighbour();
    }
    
    public void Upgrade()
    {
        Fishbowl newOne = Instantiate(AquariumManager.Instance.fishBowls[level], transform.position, Quaternion.identity).GetComponent<Fishbowl>();
        newOne.boids = boids;
        newOne.level = level+1;
        newOne.boidObjects = boidObjects;
        newOne.decoController= decoController;
        newOne.fishs = fishs;
        foreach(GameObject obj in  boidObjects)
        { 
            obj.transform.parent = newOne.transform;
            obj.GetComponent<AquariumBoids>().SetPosZero();
            obj.GetComponent<AquariumBoids>().SetMove(false);
        }
        if(decoController)
        {

            decoController.decoPositions = newOne.decoTrs;
            decoController.gameObject.transform.SetParent(newOne.transform);

        }
        Destroy(gameObject); 
        AquariumManager.Instance.facilityObj = this;
        AquariumManager.Instance.state = AquariumManager.STATE.BUILD;
        FindObjectOfType<GridManager>().ShowGrid();
    }
    private void Awake()
    {
        
        _collider = GetComponent<Collider>();
    }
}
