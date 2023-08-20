using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AddressableAssetsManager : MonoBehaviour, IManager
{
    public Dictionary<string, List<Object>> loadedObjects;
    public void InitManager()
    {
        loadedObjects = new Dictionary<string, List<Object>>();
    }

    public void ResetManager()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateManager()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            LoadAssets("Boat_01");
        }
    }
    public List<GameObject> InstantiateAssets(string label)
    {
        LoadAssets(label);
        List<GameObject> objs = new List<GameObject>();
        for(int  i  = 0; i < loadedObjects[label].Count; i ++)
        {
            GameObject obj = (GameObject)Instantiate(loadedObjects[label][i]);
            objs.Add(obj);
        }
        return objs;
    }

    public void LoadAssets(string label)
    {
        if (loadedObjects.ContainsKey(label))
        {
            Debug.Log("ÀÌ¹Ì ·Îµå µÊ");
            return;
        }
        
    }
    
}
