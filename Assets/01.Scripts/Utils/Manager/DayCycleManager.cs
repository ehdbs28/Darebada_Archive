using System.Collections;
using System.Collections.Generic;
using AssetKits.ParticleImage.Enumerations;
using UnityEngine;

public class DayCycleManager : MonoBehaviour, IManager
{
    [SerializeField]
    private Light _directionalLight;

    [SerializeField]
    private Light _reverseDirectionalLight;

    [SerializeField]
    private LightingPreset _lightingPreset;

    [SerializeField]
    private ParticleSystem _shotingStar;

    [SerializeField]
    private Transform _cloudParent;

    [SerializeField] 
    private Transform _cloudBoundFront, _cloudBoundBack;
    
    [SerializeField]
    private Material _cloudMaterial;

    private bool _isDay = false;

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

        foreach(var cloud in _cloudParent.GetComponentsInChildren<Cloud>()){
            cloud.SetUp(Random.Range(15f, 20f), _cloudBoundFront.position.z, _cloudBoundBack.position.z);
        }
    }

    public void UpdateManager()
    {
        if(_lightingPreset == null)
            return;

        _isDay = (GameManager.Instance.GetManager<TimeManager>().Hour >= 5 && GameManager.Instance.GetManager<TimeManager>().Hour <= 20);
        
        float timePercent = GameManager.Instance.GetManager<TimeManager>().Hour / 24f;
        
        PlayNightParticle();
        UpdateLighting(timePercent);
        UpdateCloudMaterial(timePercent);
    }

    private void UpdateLighting(float timePercent){
        RenderSettings.ambientLight = _lightingPreset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = _lightingPreset.FogColor.Evaluate(timePercent);
        
        if(_directionalLight != null){
            _directionalLight.color = _lightingPreset.DirectionalColor.Evaluate(timePercent);
            _directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0f));
            _reverseDirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) + 90f, 170f, 0f));
        }
    }

    private void UpdateCloudMaterial(float timePercent)
    {
        Vector2 tiling = _cloudMaterial.mainTextureScale;
        tiling.x = 2 + timePercent * 2f;
        _cloudMaterial.mainTextureScale = tiling;
    }

    private void PlayNightParticle(){
        if(_isDay){
            if(_shotingStar.isPlaying)
                _shotingStar.Stop();
        }
        else{
            if(_shotingStar.isStopped)
                _shotingStar.Play();
        }
    }

    public void ResetManager()
    {
    }
}
