using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : IManager
{
    private OceanType _type;
    private Ocean _activeOcean = null;

    public void GenerateOcean()
    {
        if (_activeOcean == null)
        {
            _activeOcean = GameManager.Instance.GetManager<PoolManager>().Pop($"{_type.ToString()}Scene") as Ocean;
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
