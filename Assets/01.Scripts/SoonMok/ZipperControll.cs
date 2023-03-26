using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngineInternal;
using static UnityEditor.PlayerSettings;

public class ZipperControll : MonoBehaviour
{
    [SerializeField] private LayerMask _zipperLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButton(0))
        {
            if(Physics.Raycast(pos,Vector3.forward, out hit, 20f,_zipperLayer))
            {
                hit.collider.GetComponent<Fronts>().Hold(true);
            }

        }else if(Input.GetMouseButtonUp(0)) 
        {
            if(Physics.Raycast(pos, Vector3.forward, out hit, 20f, _zipperLayer))
            {
                hit.collider.GetComponent<Fronts>().Hold(false);
            }
        }
    }
}
