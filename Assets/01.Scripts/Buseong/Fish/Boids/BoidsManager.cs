using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoidsManager : MonoBehaviour, IManager
{
    public static BoidsManager Instance;

    private BoidUnit boidUnitPrefab;
    public List<GameObject> Fishes;

    private FishDataUnit _fishData;

    private float _total;
    [SerializeField]
    private float spawnRange;

    private Boids _currentBoid;

    private FishDataTable _fishDataTable;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("ERROR : Multiple BoidManager is Running");
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        Fishes = new List<GameObject>();
        _fishDataTable = (FishDataTable)GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.FishData);
    }
    
    private void Update()
    {

    }
    
    public void SpawnBoid(OceneType type)
    {
        GameObject boid = new GameObject();
        boid = Instantiate(boid.gameObject, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
        boid.AddComponent<Boids>();
        boid.GetComponent<Boids>().Setup(type, _fishDataTable);
    }

    public void SpawnFish()
    {
        float randomPoint = Random.value * _total;
        //Debug.Log($"ÇöÀç ·£´ý °ª: {randomPoint}");
        for (int j = 0; j < _fishDataTable.Size; j++)
        {
            if (randomPoint <= _fishDataTable.DataTable[j].SpawnPercent)
            {
                _fishData = _fishDataTable.DataTable[j];
                break;
            }
            else
            {
                randomPoint -= _fishDataTable.DataTable[j].SpawnPercent;
            }
        }
        Vector3 randomVec = Random.insideUnitSphere;
        //Vector3 randomVec = Random.insideUnitSphere - NonSpawnArea.transform.position;
        randomVec *= spawnRange;
        randomVec.y -= spawnRange * (_fishData.Rarity - 1);
        Quaternion randomRot = Quaternion.Euler(0, Random.Range(0, 360f), 0);
        BoidUnit currUnit = Instantiate(boidUnitPrefab, this.transform.position + randomVec, randomRot);
        currUnit.transform.SetParent(this.transform);
        Vector3 offset = new Vector3(0f, randomVec.y, 0f);
        currUnit.InitializeUnit(_currentBoid, _fishData.Speed, _fishData, offset);
        Fishes.Add(currUnit.gameObject);
    }

    public void ChangeBoid(Boids boids)
    {
        _currentBoid = boids;
    }

    public void ResetManager()
    {

    }

    public void InitManager()
    {

    }

    public void UpdateManager()
    {

    }
}