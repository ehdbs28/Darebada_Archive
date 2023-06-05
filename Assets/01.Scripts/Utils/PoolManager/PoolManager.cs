using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : IManager
{
    private Dictionary<string, Pool<PoolableMono>> _pools = new Dictionary<string, Pool<PoolableMono>>();

    private Transform _trmParent;

    public PoolManager(Transform trmParent)
    {
        _trmParent = trmParent;
    }

    public void CreatePool(PoolableMono prefab, int count = 10)
    {
        Pool<PoolableMono> pool = new Pool<PoolableMono>(prefab, _trmParent, count);
        _pools.Add(prefab.gameObject.name, pool);
    }


    public PoolableMono Pop(string prefabName)
    {
        if (!_pools.ContainsKey(prefabName))
        {
            Debug.Log($"Prefab does not exist on pool : {prefabName}");
            return null;
        }
        PoolableMono item = _pools[prefabName].Pop();
        item.Init();
        return item;
    }

    public void Push(PoolableMono obj)
    {
        _pools[obj.name].Push(obj);
    }

    public void InitManager(){}
    public void UpdateManager(){}
    public void ResetManager(){}
}