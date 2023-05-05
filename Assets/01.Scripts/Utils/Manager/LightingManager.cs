using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour, IManager
{
    [SerializeField]
    private Light _directionalLight;

    [SerializeField]
    private LightingPreset _lightingPreset;

    public void InitManager()
    {
    }

    public void UpdateManager()
    {
    }

    public void Reset()
    {
    }
}
