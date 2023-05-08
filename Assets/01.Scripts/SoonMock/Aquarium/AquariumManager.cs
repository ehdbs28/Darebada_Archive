using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquariumManager : MonoBehaviour
{
    #region "자원 및 요소들"

    public long Gold
    {
        get { return MoneyManager.Instance.money; }
        set { MoneyManager.Instance.money = value; }
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
    [SerializeField] private float _entrancePercent;
    public float EntrancePercent
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
    [SerializeField] GameObject _fishBowlObject;
    [SerializeField] GameObject _fishObject;
    [SerializeField] GameObject _decoObject;
    [SerializeField] GameObject _snackShopObject;
    [SerializeField] GameObject _buildPanel;
    public int decoCount = 0;
    public List<GameObject> aquaObject = new List<GameObject>();
    public List<Facility> aquarium = new List<Facility>();

    [SerializeField] private Facility _facilityObj;

    [SerializeField] private BuildFacility _build;

    public enum STATE
    {
        NORMAL,
        BUILD
    }
    public STATE state = STATE.NORMAL;
    public InputManager inputManager;
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
        inputManager = FindObjectOfType<InputManager>();
        _build = GetComponent<BuildFacility>();
    }
    private void Update()
    {
        EntrancePercent = Mathf.Clamp((float)((float)aquaObject.Count/(float)EntranceFee)*100f,0f,200f);
        Reputation = (EntrancePercent/100f * CleanScore/100f * ArtScore/100f) * 100f;
        ArtScore = Mathf.Clamp(((float)(decoCount/2)/ aquarium.Count) * 100, 0, 100);
        if(state == STATE.BUILD)
        {
            if(Input.GetMouseButton(0) && GameManager.Instance.GetManager<InputManager>().MousePositionToGroundRayPostion != Vector3.zero )
            {
                _facilityObj.transform.position =  _build.GetFacilityPos();
            }
        }
    }
    public void SetFacilityPos()
    {
        Debug.Log(_facilityObj.transform.position);

        if (_facilityObj.CheckCollision()) 
        {

            state = STATE.NORMAL;
            aquaObject.Add(_facilityObj.gameObject);
            if (_facilityObj.GetComponent<Fishbowl>())
            {
                aquarium.Add(_facilityObj);
            }
            _facilityObj = null;
        }
    }
    public void AddFishBowl()
    {
        _buildPanel.SetActive(false);
        Fishbowl fishBowl = Instantiate(_fishBowlObject).GetComponent<Fishbowl>();
            _floorSize.z = aquaObject.Count % _horizontalCount * 5;
        if(aquaObject.Count%_horizontalCount==0)
        {
            Debug.Log("Asdf");
            _floorSize.x += 5;
        }
        fishBowl.transform.localPosition = new Vector3(_floorSize.x, 0, _floorSize.z);
        _facilityObj = fishBowl;

        state = STATE.BUILD;
        //    else floor.transform.localPosition = new Vector3(_floorSize.x / 2+10, 0, 0);
    }
    public void AddSnackShop()
    {
        _buildPanel.SetActive(false);
        SnackShop snackShop = Instantiate(_snackShopObject).GetComponent<SnackShop>();
        _floorSize.z = aquaObject.Count % _horizontalCount * 5;
        if (aquaObject.Count % _horizontalCount == 0)
        {
            Debug.Log("Asdf");
            _floorSize.x += 5;
        }
        snackShop.transform.localPosition = new Vector3(_floorSize.x, 0.5f, _floorSize.z);
        _facilityObj = snackShop;
        state = STATE.BUILD;
    }
    public void ChangeFacilityPos(Facility obj)
    {
        _facilityObj = obj;
        state = STATE.BUILD;
    }
    public GameObject AddFish(int id, Transform parent)
    {
        GameObject fish = Instantiate(_fishObject);
        fish.transform.parent = parent;
        fish.transform.localPosition = new Vector3(Random.Range(-0.8f,0.8f), 1f, Random.Range(-0.8f, 0.8f));
        return fish;
    }
    public GameObject AddDeco(int id, Transform parent)
    {
        GameObject deco = Instantiate(_decoObject);
        deco.transform.parent = parent;
        deco.GetComponent<Deco>().SetId(id);
        deco.transform.localPosition = deco.GetComponent<Deco>().pos;
        return deco;
    }
    
}
