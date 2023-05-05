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
        if(RenderSettings.sun != null){
            _directionalLight = RenderSettings.sun;
        }
        else{
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(var light in lights){
                if(light.type == LightType.Directional){
                    _directionalLight = light;
                    return;
                }
            }
        }
    }

    public void UpdateManager()
    {
        if(_lightingPreset == null)
            return;

        UpdateLighting(GameManager.Instance.GetManager<TimeManager>().Hour / 24);
    }

    private void UpdateLighting(float timePercent){
        RenderSettings.ambientLight = _lightingPreset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = _lightingPreset.FogColor.Evaluate(timePercent);
        
        if(_directionalLight != null){
            _directionalLight.color = _lightingPreset.DirectionalColor.Evaluate(timePercent);
            _directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0f)); 
        }
    }

    public void Reset()
    {
    }
}
