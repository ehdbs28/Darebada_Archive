using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BiomeManager : MonoBehaviour
{
    public static BiomeManager Instance;

    public List<GameObject> _biomes;
    public List<FishSO> _fishes;

    public float _depth;

    public GameObject _fishSpawner;

    private const int _fishCount = 50;
    private Vector3 _spawnArea;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multy BiomeManager");
        }
        Instance = this;
    }

    private void Start()
    {
        SpawnBiome(BIOME.CLOUD);
    }

    private void Update()
    {
        RespawnFish();
    }

    private float RandomCalc() => Random.Range(0, 10001) / 100.0f;

    public void SpawnBiome(BIOME biome)
    {
        GameObject spawnBiome = Instantiate(_biomes[(int)biome]);
        //_spawnArea = spawnBiome.transform.localScale;
        _spawnArea = new Vector3(500, 0, 500);

        for (int i = 0; i < _fishCount; i++)
        {
            SpawnFish();
        }
    }

    private void SpawnFish()
    {
        float spawnheight = Random.Range(_depth, 0);
        SpawnRandomFish(spawnheight, spawnheight / (_depth / 5));
    }

    private void SpawnRandomFish(float spawnheight, float extra)
    {
        float rand = RandomCalc();

        Vector3 spawnVec = new Vector3();
        spawnVec.x = Random.Range(-_spawnArea.x/2, _spawnArea.x/2);
        spawnVec.y = spawnheight;
        spawnVec.z = Random.Range(-_spawnArea.z/2, _spawnArea.z/2);
        
        if (rand <= 0.1f * extra)
        {
            Instantiate(FindRarityFish(5)._fishPrefab, spawnVec, Quaternion.identity, _fishSpawner.transform); 
        }
        else if (rand <= 1.0f * extra) 
        { 
            Instantiate(FindRarityFish(4)._fishPrefab, spawnVec, Quaternion.identity, _fishSpawner.transform); 
        }
        else if (rand <= 5.0f * extra) 
        { 
            Instantiate(FindRarityFish(3)._fishPrefab, spawnVec, Quaternion.identity, _fishSpawner.transform); 
        }
        else if (rand <= 30.0f * extra) 
        {
            Instantiate(FindRarityFish(2)._fishPrefab, spawnVec, Quaternion.identity, _fishSpawner.transform);
        }
        else if (rand <= 100.0f * extra) 
        { 
            Instantiate(FindRarityFish(1)._fishPrefab, spawnVec, Quaternion.identity, _fishSpawner.transform);
        }
    }
    
    private FishSO FindRarityFish(int rarity)
    {
        List<FishSO> fishes = new List<FishSO>(); 

        for (int i = 0; i < _fishes.Count; i++)
        {
            if (rarity == _fishes[i].Rarity)
            {
                fishes.Add(_fishes[i]);
            }
        }

        return fishes[Random.Range(0, fishes.Count)];
    }
    
    private void RespawnFish()
    {
        int curFishCount = _fishSpawner.transform.childCount;

        if (curFishCount == _fishCount) return;

        for (int i = 0; i < _fishCount - curFishCount; i++)
        {
            SpawnFish();
        }
    }
    // 계속 리스폰 만들기
}