using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masking : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.renderQueue = 3002;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
