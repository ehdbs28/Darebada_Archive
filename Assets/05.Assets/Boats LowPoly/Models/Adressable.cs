using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
public class Adressable : MonoBehaviour
{
    
    [SerializeField] List<GameObject> serObject;
    // Start is called before the first frame update
    void Start()
    {
        // LoadAssets();
        InstantiateAsset();
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
