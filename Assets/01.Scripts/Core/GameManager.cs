using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private List<IManager> _managers;

    [SerializeField]
    private const float _autoSaveDelay = 3f;

    [SerializeField]
    private Transform _poolingTrm;

    [SerializeField]
    private PoolingListSO _poolingList;

    [SerializeField]
    private GameSceneType _startSceneType;

    private void Awake() {
        if(Instance != null){
            Debug.Log("ERROR : Multiple GameManager is Running");
            return;
        }
        Instance = this;

        _managers = new List<IManager>();
        AddManager();

        //_managers.ForEach(manager => manager.InitManager());
    }   

    private void Start() {
        GetManager<GameSceneManager>().ChangeScene(_startSceneType);
        StartCoroutine(AutoSave(_autoSaveDelay));
    }

    private void Update() {
        foreach(var manager in _managers){
            //manager.UpdateManager();
        }
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    private void AddManager(){
        _managers.Add(new GameSceneManager());
        _managers.Add(new DataManager());
        _managers.Add(GetComponent<SheetDataManager>());
        _managers.Add(new MoneyManager());
        _managers.Add(GetComponent<InputManager>());
        _managers.Add(GetComponent<CameraManager>());
        _managers.Add(new TimeManager());
        _managers.Add(transform.Find("UIManager").GetComponent<UIManager>());
        //_managers.Add(GetComponent<DayCycleManager>());
        _managers.Add(new AddressableAssetsManager());
        _managers.Add(new FishingUpgradeManager());
        _managers.Add(new BoatManager());
        _managers.Add(GetComponent<LetterManager>());
        _managers.Add(new PoolManager(_poolingTrm));
        _poolingList.Pairs.ForEach(pair => GetManager<PoolManager>().CreatePool(pair.Prefab, pair.Count));
    }

    private IEnumerator AutoSave(float delay){
        while(true){
            DataManager dataManager = GetManager<DataManager>();
            GameData gameData = dataManager.GetData(DataType.GameData) as GameData;

           // gameData.LastWorldTime = GetManager<TimeManager>();

            // ?�?????�이????추�??�야 ?�긴 ??
            yield return new WaitForSecondsRealtime(delay);
        }
    }

    public T GetManager<T>() where T : IManager{
        T returnValue = default(T);

        foreach(var manager in _managers.OfType<T>()){
            returnValue = manager;
        }

        return returnValue;
    }
}
