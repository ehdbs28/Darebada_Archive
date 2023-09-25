using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoidsManager : MonoBehaviour, IManager
{
    private BoidUnit boidUnitPrefab;
    public List<GameObject> Fishes;

    private FishDataUnit _fishData;

    private float _total;
    [SerializeField]
    private float spawnRange;

    private Boids _currentBoid;

    private FishDataTable _fishDataTable;

    public void InitManager()
    {
        Fishes = new List<GameObject>();
        _fishDataTable = (FishDataTable)GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.FishData);
    }
    
    public void SpawnBoid(OceanType type)
    {
        GameObject boid = new GameObject();
        boid = Instantiate(boid.gameObject, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
        boid.AddComponent<Boids>();
        boid.GetComponent<Boids>().Setup(type, _fishDataTable);
    }

    public void ResetManager()
    {

    }
    
    public void UpdateManager()
    {

    }
}