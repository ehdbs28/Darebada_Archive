using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    private BoatActionData _boatData;
    public BoatActionData BoatData => _boatData;

    private readonly List<CommonModule> _modules = new List<CommonModule>();

    private void Awake() {
        _boatData = GetComponent<BoatActionData>();

        transform.Find("Modules").GetComponentsInChildren<CommonModule>(_modules);
    }

    private void Start() {        
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
