using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : IManager
{
    private OceanType _type;
    private Ocean _activeOcean = null;

    public AudioClip _bgmClip;
    
    private FishDataTable _dataTable;

    private List<BoxCollider> _boundColliders;

    public List<BoxCollider> BoundColliders
    {
        get => _boundColliders;
        set
        {
            _boundColliders = value;
            _boundHeight = _boundColliders[0].bounds.size.y;
        }
    }

    private List<FishDataUnit> _spawnableFishDatas = new List<FishDataUnit>();
    private List<OceanFishController> _currentSpawnFishes = new List<OceanFishController>();
    
    private float _totalSpawnPercent;

    private float _boundHeight;

    private const int MaxFishCount = 50;
    
    public void InitManager()
    {
        _dataTable = GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.FishData) as FishDataTable;
    }

    public void UpdateManager()
    {
    }
    
    public void GenerateOcean()
    {
        if (_activeOcean == null)
        {
            _spawnableFishDatas.Clear();
            _currentSpawnFishes.Clear();
            
            _totalSpawnPercent = 0f;
            for (int i = 0; i < _dataTable.Size; i++)
            {
                _totalSpawnPercent += _dataTable.DataTable[i].SpawnPercent;
            }
            _activeOcean = GameManager.Instance.GetManager<PoolManager>().Pop($"{_type.ToString()}Scene") as Ocean;

            _bgmClip = _activeOcean.gameObject.GetComponent<AudioSource>().clip;
            GameManager.Instance.GetManager<SoundManager>().Play(_bgmClip, SoundEnum.BGM);
        }
    }

    public void RemoveOcean()
    {
        if (_activeOcean != null)
        {
            GameManager.Instance.GetManager<PoolManager>().Push(_activeOcean);
            _activeOcean = null;
        }
    }

    public void SetType(OceanType type)
    {
        _type = type;
    }

    public void RemoveFish()
    {
        for (int i = 0; i < _currentSpawnFishes.Count; i++)
        {
            _currentSpawnFishes[i].CompleteSetting = false;
            GameManager.Instance.GetManager<PoolManager>().Push(_currentSpawnFishes[i]);
        }
    }

    public void GenerateFish()
    {
        for (int i = 0; i < _dataTable.Size; i++)
        {
            if (_dataTable.DataTable[i].Habitat == _type)
            {
                _spawnableFishDatas.Add(_dataTable.DataTable[i]);
            }
        }

        for (int i = 0; i < MaxFishCount; i++)
        {
            SpawnFishElement();
        }
    }

    private void SpawnFishElement()
    {
        float randomPoint = Random.value * _totalSpawnPercent;
        FishDataUnit fishData = null;
        
        for (int i = 0; i < _spawnableFishDatas.Count; i++)
        {
            if (randomPoint <= _spawnableFishDatas[i].SpawnPercent)
            {
                fishData = _spawnableFishDatas[i];
                break;
            }
            else
            {
                randomPoint -= _spawnableFishDatas[i].SpawnPercent;
            }
        }

        if (fishData == null)
            return;

        Vector3 pos = GetRandomPos(fishData);
        Quaternion rot = GetRandomRot();

        OceanFishController fish = GameManager.Instance.GetManager<PoolManager>().Pop("FishTemplete") as OceanFishController;
        fish.SetPositionAndRotation(pos, rot);
        fish.SetUp(fishData, _boundColliders[fishData.Rarity - 1]);
        
        _currentSpawnFishes.Add(fish);
    }

    private Vector3 GetRandomPos(FishDataUnit data)
    {
        BoxCollider bound = _boundColliders[data.Rarity - 1];
        Bounds bounds = bound.bounds;

        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    private Quaternion GetRandomRot()
    {
        return Quaternion.Euler(0, Random.Range(0, 360f), 0);
    }
    
    public void ResetManager()
    {
    }
}
