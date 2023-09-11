using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private List<IManager> _managers;
    public List<IManager> Managers => _managers;

    [SerializeField]
    private const float _autoSaveDelay = 3f;

    [SerializeField]
    private Transform _poolingTrm;

    [SerializeField]
    private PoolingListSO _poolingList;

    [SerializeField]
    private GameSceneType _startSceneType;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("ERROR : Multiple GameManager is Running");
            return;
        }
        Instance = this;

        _managers = new List<IManager>();
        AddManager();

        _managers.ForEach(manager => manager.InitManager());
    }

    private void Start()
    {
        GetManager<GameSceneManager>().ChangeScene(_startSceneType);
        StartCoroutine(AutoSave(_autoSaveDelay));
    }

    private void Update()
    {
        foreach (var manager in _managers)
        {
            manager.UpdateManager();
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void AddManager()
    {
        _managers.Add(new GameSceneManager());
        _managers.Add(new DataManager());
        _managers.Add(GetComponent<SheetDataManager>());
        _managers.Add(new MoneyManager());
        _managers.Add(GetComponent<InputManager>());
        _managers.Add(GetComponent<CameraManager>());
        _managers.Add(new TimeManager());
        _managers.Add(transform.Find("UIManager").GetComponent<UIManager>());
        _managers.Add(transform.Find("SoundManager").GetComponent<SoundManager>());
        _managers.Add(GetComponent<DayCycleManager>());
        // _managers.Add(new AddressableAssetsManager());
        _managers.Add(new FishingUpgradeManager());
        _managers.Add(new SeleteItemManager());
        _managers.Add(GetComponent<MiniGameManager>());
        _managers.Add(GetComponent<BoatManager>());
        _managers.Add(new OceanManager());
        _managers.Add(GetComponent<BoidsManager>());
        _managers.Add(GetComponent<LetterManager>());
        _managers.Add(new ShopManager());
        _managers.Add(new PoolManager(_poolingTrm));
        _managers.Add(new InventoryManager());
        _managers.Add(new LoadingManager());
        _managers.Add(new SettingManager());
        //_managers.Add(GetComponent<AquariumNumericalManager>());
        _managers.Add(new ChallengeManager());
        _managers.Add(GetComponent<TutorialManager>());
        _poolingList.Pairs.ForEach(pair => GetManager<PoolManager>().CreatePool(pair.Prefab, pair.Count));
    }

    private IEnumerator AutoSave(float delay)
    {
        while (Application.isPlaying)
        {
            GetManager<DataManager>().SaveDataAll();
            yield return new WaitForSecondsRealtime(delay);
        }
    }

    public T GetManager<T>() where T : IManager
    {
        T returnValue = default(T);

        foreach (var manager in _managers.OfType<T>())
        {
            returnValue = manager;
        }

        return returnValue;
    }
}
