using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
    public static BoidsManager Instance;

    private BoidUnit boidUnitPrefab;
    private List<FishSO> _unitPrefabList;
    public List<GameObject> Fishes;

    private FishSO _fishData;

    private float _total;
    [SerializeField]
    private float spawnRange;

    private Boids _currentBoid;

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
        _unitPrefabList = new List<FishSO>();
    }
    
    private void Update()
    {

    }

    public void SpawnFish()
    {
        float randomPoint = Random.value * _total;
        //Debug.Log($"ÇöÀç ·£´ý °ª: {randomPoint}");
        for (int j = 0; j < _unitPrefabList.Count; j++)
        {
            if (randomPoint <= _unitPrefabList[j].SpawnPercentage)
            {
                _fishData = _unitPrefabList[j];
                break;
            }
            else
            {
                randomPoint -= _unitPrefabList[j].SpawnPercentage;
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
}