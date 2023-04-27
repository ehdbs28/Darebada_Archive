using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    private BoatData _boatData;
    public BoatData BoatData => _boatData;

    private readonly List<CommonModule> _modules = new List<CommonModule>();

    private void Awake() {
        _boatData = GetComponent<BoatData>();

        transform.Find("Modules").GetComponentsInChildren<CommonModule>(_modules);

        foreach(var module in _modules){
            module.SetUp(transform);
        }
    }

    private void Update() {
        foreach(var module in _modules){
            module.UpdateModule();
        }
    }

    private void FixedUpdate() {
        foreach(var module in _modules){
            module.FixedUpdateModule();
        }
    }

    public T GetModule<T>() where T : CommonModule{
        T returnModule = null;

        foreach(var module in _modules.OfType<T>()){
            returnModule = module;
        }

        return returnModule;
    }
}
