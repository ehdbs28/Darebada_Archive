using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleController : PoolableMono
{
    protected readonly List<Module> _modules = new List<Module>();

    protected virtual void Awake() {
        transform.Find("Modules").GetComponentsInChildren<Module>(_modules);
    }

    protected virtual void Start() {        
        foreach(var module in _modules){
            module.SetUp(transform);
        }
    }

    protected virtual void Update() {
        foreach(var module in _modules){
            module.UpdateModule();
        }
    }

    protected virtual void FixedUpdate() {
        foreach(var module in _modules){
            module.FixedUpdateModule();
        }
    }

    public T GetModule<T>() where T : Module {
        T returnModule = default(T);

        foreach(var module in _modules.OfType<T>()){
            returnModule = module;
        }

        return returnModule;
    }

    public override void Init()
    {
    }
}