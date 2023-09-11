using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : MonoBehaviour
{
    public List<DecoVisualSO> soList;
    public List<MeshFilter> meshs;
    private void Awake()
    {
        for(int  i = 0; i < soList.Count; i++)
        {
            soList[i].mesh = meshs[i].sharedMesh;
        }
    }
}
