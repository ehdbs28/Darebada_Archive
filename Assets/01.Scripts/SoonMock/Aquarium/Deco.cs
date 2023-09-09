using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;


public class Deco : MonoBehaviour
{
    public DecoVisualSO visualSO;
    public void Initialize(DecoVisualSO visual)
    {
        SkinnedMeshRenderer mr = GetComponent<SkinnedMeshRenderer>();
        visualSO = visual;
        mr.material = visual.mainMat;
        mr.sharedMesh= visual.mesh;
        gameObject.name = visual.Name;
    }
}
