using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanSubManager : MonoBehaviour
{
    [SerializeField] 
    private List<BoxCollider> _bounds;

    public void EnterSceneEvent()
    {
        if (GameManager.Instance.GetManager<OceanManager>().BoundColliders == null)
        {
            GameManager.Instance.GetManager<OceanManager>().BoundColliders = _bounds;
        }
            
        GameManager.Instance.GetManager<OceanManager>().GenerateOcean();
        GameManager.Instance.GetManager<OceanManager>().GenerateFish();
    }

    public void ExitSceneEvent()
    {
        GameManager.Instance.GetManager<OceanManager>().RemoveOcean();
        GameManager.Instance.GetManager<OceanManager>().RemoveFish();
    }
}
