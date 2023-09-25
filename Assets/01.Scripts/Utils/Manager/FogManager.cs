using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FogManager : IManager
{
    public FogSO oldSO;
    public void ResetManager()
    {
    }

    public void InitManager()
    {
    }

    public void UpdateManager()
    {
    }
    public void FogOn(FogSO fogSO = null)
    {
        RenderSettings.fog = true;
        if(fogSO != null)
        {
            RenderSettings.fogColor = fogSO.color;
            RenderSettings.fogStartDistance = fogSO.startValue;
            RenderSettings.fogEndDistance= fogSO.endValue;
        }
        else
        {
            RenderSettings.fogColor = oldSO.color;
            RenderSettings.fogStartDistance = oldSO.startValue;
            RenderSettings.fogEndDistance = oldSO.endValue;

        }
    }
    public void FogOff()
    {
        RenderSettings.fog = false;
    }
}
