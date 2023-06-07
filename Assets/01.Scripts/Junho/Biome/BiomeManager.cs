using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeManager : MonoBehaviour
{
    public static BiomeManager Instance;

    public List<GameObject> _biomes;
    public List<FishSO> _fishes;

    private const int _fishCount = 50;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multy BiomeManager");
        }
        Instance = this;
    }

    private float RandomCalc() => Random.Range(0, 10001) / 100.0f;
    
    public void SpawnBiome(BIOME biome)
    {
        GameObject spawnBiome = Instantiate(_biomes[(int)biome]);

        SpawnRandomFishes();
    }

    private void SpawnRandomFishes()
    {
        for (int i = 0; i < _fishCount; i++)
        {
            float rand = RandomCalc();
            if (rand <= 1.0f)
            {

            }
            else if (rand <= 5.0f)
            {

            }
            else if (rand <= 10.0f)
            {

            }
            else if (rand <= 50.0f)
            {

            }
            else if (rand <= 100.0f)
            {

            }
        }
        // 희귀도에 따라 랜덤 50마리 스폰
    }
    
    // 계속 리스폰 만들기
}