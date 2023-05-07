using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public static FishManager Instance;

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
        if (Instance != null)
        {
            Debug.LogError("Multy MiniGameManager");
        }
        Instance = this;

        ChangeBiome(_currentBiome);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeBiome(_nextBiomeTest);
        }

        if(_fishes.Count < _maxFishNum)
        {
            CreateFish();
        }
    }

    public void ChangeBiome(BIOME selectBiome)
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
            // ���⼭ ������ �����͸� ã�Ƽ�
            // ���� )
            // ���� ���̿ȿ��� ���� �� ��� fish�� ���� Ȯ���� ���� 100�̴�.
            // _currentBiomeFishedData�� ������������ ���� �Ǿ� �־�� �Ѵ�.
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

    public void DeleteFish(GameObject fish){
        int index = _fishes.IndexOf(fish);
        Destroy(fish);
        _fishes.RemoveAt(index);
    }
}
