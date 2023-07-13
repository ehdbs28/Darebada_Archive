using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
public class Adressable : MonoBehaviour
{
    
    [SerializeField] List<GameObject> serObject;
    // Start is called before the first frame update
    void Start()
    {
        // LoadAssets();
        InstantiateAsset();
    }
    AsyncOperationHandle<IList<GameObject>> handle;
    public void LoadAssets()
    {
        handle = new AsyncOperationHandle<IList<GameObject>>();
        for (int i = 1;  i <=5; i ++)
        {
            handle = Addressables.LoadAssetsAsync<GameObject>("Boat_0" + i.ToString(), (GameObject temp) =>
            {
                serObject.Add(temp);
            });
        }

    }
    public void InstantiateAsset()
    {

        for (int i = 0; i < 5; i++)
        {
            Instantiate(serObject[i], transform.position, Quaternion.identity); 
        }
    }
    // Update is called once per frame
    void Update()
    {
        //if(handle.IsDone)
        //{
        //    InstantiateAsset(); 
        //    this.enabled= false;
        //}
    }
}
