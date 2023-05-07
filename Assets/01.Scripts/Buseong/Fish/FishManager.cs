using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    [SerializeField]
    private BIOME _currentBiome;
    [SerializeField]
    private BIOME _nextBiomeTest = BIOME.TEST1;

    [SerializeField]
    public List<GameObject> _fishes;
    [SerializeField]
    private List<FishSO> _fishDatas;
    [SerializeField]
    private List<FishSO> _currentBiomeFishesData;

    [SerializeField]
    private GameObject _fishPrefabs;

    [SerializeField]
    private float _maxFishNum = 30f;

    private void Awake()
    {
        ChangeBiome(_currentBiome);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeBiome(_nextBiomeTest);
        }

        /*if(_fishes.Count < _maxFishNum)
        {
            CreateFish();
        }*/

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateFish();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            int rand = Random.Range(0, _fishes.Count);
            Destroy(_fishes[rand].gameObject);
            _fishes.RemoveAt(rand);
        }
    }

    private void ChangeBiome(BIOME selectBiome)
    {
        _currentBiome = selectBiome;
        for(int i = 0; i < _fishDatas.Count; i++)
        {
            if(_fishDatas[i].HabitatBiome == _currentBiome)
            {
                _currentBiomeFishesData.Add(_fishDatas[i]);
            }
        }
    }

    private void CreateFish()
    {
        FishSO spawnFishData = null;
        float targetPercent = Random.Range(0, 100);
        float sumPercent = 0f;
        
        foreach(var data in _currentBiomeFishesData)
        {
            // 여기서 최적의 데이터를 찾아서
            // 가정 )
            // 현재 바이옴에서 생성 될 모든 fish의 스폰 확률의 합은 100이다.
            // _currentBiomeFishedData는 내림차순으로 정렬 되어 있어야 한다.
            sumPercent += data.SpawnPercent;

            if(sumPercent >= targetPercent)
            {
                spawnFishData = data;
                break;
            }
        }

        //Fish newFish = new Fish(spawnFishData);
        Fish newFish = Instantiate(_fishPrefabs, new Vector3(Random.Range(0, 50), Random.Range(0, 50), Random.Range(0, 50)), Quaternion.identity).GetComponent<Fish>();
        newFish.Init(spawnFishData);
        newFish.GetComponent<MeshRenderer>().material = spawnFishData.TestMat;
        _fishes.Add(newFish.gameObject);
    }
}
