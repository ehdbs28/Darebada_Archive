using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanSubManager : MonoBehaviour
{
    public void EnterSceneEvent()
    {
        GameManager.Instance.GetManager<OceanManager>().GenerateOcean();
    }

    public void ExitSceneEvent()
    {
        GameManager.Instance.GetManager<OceanManager>().RemoveOcean();
    }
}
