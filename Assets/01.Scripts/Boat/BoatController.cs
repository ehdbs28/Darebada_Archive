using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [SerializeField]
    private BoatData _boatData;
    public BoatData BoatData => _boatData;

    private readonly List<CommonModule> _modules = new List<CommonModule>();

    private void Awake() {
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
}
