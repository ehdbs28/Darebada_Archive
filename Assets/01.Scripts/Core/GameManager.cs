using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private List<IManager> _managers;

    private void Awake() {
        if(Instance != null){
            Debug.Log("ERROR : Multiple GameManager is Running");
            return;
        }
        Instance = this;

        _managers = new List<IManager>();
        AddManager();

        _managers.ForEach(manager => manager.InitManager());
    }   

    private void Update() {
        foreach(var manager in _managers){
            manager.UpdateManager();
        }
    }

    private void AddManager(){
        _managers.Add(new DataManager());
        _managers.Add(GetComponent<InputManager>());
        _managers.Add(GetComponent<CameraManager>());
    }

    public T GetManager<T>() where T : IManager{
        T returnValue = default(T);

        foreach(var manager in _managers.OfType<T>()){
            returnValue = manager;
        }

        return returnValue;
    }
}
