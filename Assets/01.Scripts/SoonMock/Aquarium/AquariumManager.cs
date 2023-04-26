using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquariumManager : MonoBehaviour
{
    #region "자원 및 요소들"

    [SerializeField] private int _gold;
    public int Gold
    {
        get { return _gold; }
        set { _gold = value; }
    }
    [SerializeField] private int _cleanScore;
    public int CleanScore
    {
        get { return _cleanScore; }
        set { _cleanScore = value; }
    }
    [SerializeField] private int _entrancefee;
    public int EntranceFee
    {
        get { return _entrancefee; }
        set { _entrancefee = value; }
    }
    [SerializeField] private int _entrancePercent;
    public int EntrancePercent
    {
        get { return _entrancePercent; }
        set { _entrancePercent= value; }
    }
    [SerializeField] private float _reputation;
    public float Reputation
    {
        get { return _reputation; }
        set { _reputation = value; }
    }
    [SerializeField] private float _artScore;
    public float ArtScore
    {
        get { return _artScore; }
        set { _artScore = value; }
    }
    #endregion
    //싱글톤
    public static AquariumManager Instance;

    //크기 관련
    [SerializeField] GameObject floor;
    [SerializeField] Vector3 _floorSize;
    [SerializeField] private int _horizontalCount = 5;
    
    //아쿠아리움 내 시설들
    [SerializeField] GameObject fishBowlObject;
    [SerializeField] GameObject fishObject;
    [SerializeField] GameObject snackShopObject;
    public List<GameObject> aquaObject = new List<GameObject>();
    public List<Facility> aqurium = new List<Facility>();


    private void Awake()
    {
        if(aquaObject.Count <=0)
        {
            AddFishBowl();

        }
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);
    }
    private void Update()
    {
        EntrancePercent = Mathf.Clamp((aquaObject.Count/EntranceFee)*100,0,100);
        Reputation = (EntrancePercent/100f * CleanScore/100f * ArtScore/100f) * 100f;
        if(Input.GetKeyDown(KeyCode.K))
        {
            AddFishBowl();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            AddSnackShop();
        }
    }
    public void AddFishBowl()
    {
        Fishbowl fishBowl = Instantiate(fishBowlObject).GetComponent<Fishbowl>();
            _floorSize.z = aqurium.Count % _horizontalCount * 5;
        if(aqurium.Count%_horizontalCount==0)
        {
            Debug.Log("Asdf");
            _floorSize.x += 5;
        }
        fishBowl.transform.localPosition = new Vector3(_floorSize.x, 0, _floorSize.z);
        aquaObject.Add(fishBowl.gameObject);
        aqurium.Add(fishBowl);
        if (aquaObject.Count % _horizontalCount != 0)
        {
        floor.transform.localScale = new Vector3(aquaObject.Count / _horizontalCount * 5 + 5, 0.5f, (_horizontalCount + 1) *5);
            floor.transform.localPosition = new Vector3(_floorSize.x / 2 + 5, 0.5f, (_horizontalCount + 1) *1.5f);

        }
    //    else floor.transform.localPosition = new Vector3(_floorSize.x / 2+10, 0, 0);
    }
    public void AddSnackShop()
    {
        SnackShop snackShop = Instantiate(snackShopObject).GetComponent<SnackShop>();
        _floorSize.z = aqurium.Count % _horizontalCount * 5;
        if (aqurium.Count % _horizontalCount == 0)
        {
            Debug.Log("Asdf");
            _floorSize.x += 5;
        }
        snackShop.transform.localPosition = new Vector3(_floorSize.x, 0.5f, _floorSize.z);
        aquaObject.Add(snackShop.gameObject);
        aqurium.Add(snackShop);
        if (aquaObject.Count % _horizontalCount != 0)
        {
            floor.transform.localScale = new Vector3(aquaObject.Count / _horizontalCount * 5 + 5, 0.5f, (_horizontalCount + 1) * 5);
            floor.transform.localPosition = new Vector3(_floorSize.x / 2 + 5, 0.5f, (_horizontalCount + 1) * 1.5f);

        }
    }
    public GameObject AddFish(int id, Transform parent)
    {
        GameObject fish = Instantiate(fishObject);
        fish.transform.parent = parent;
        fish.transform.localPosition = new Vector3(Random.Range(-0.8f,0.8f), 1f, Random.Range(-0.8f, 0.8f));
        return fish;
    }
    }
