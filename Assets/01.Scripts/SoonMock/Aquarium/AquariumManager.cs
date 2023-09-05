using Core;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AquariumManager : MonoBehaviour, IManager
{
    public Material fishBowlMat;
    public Material mossMat;
    public Transform facilityParent;
    [SerializeField] Color pureWaterColor;
    [SerializeField] Color corruptedWaterColor;
    #region "�ڿ� �� ��ҵ�"
    [SerializeField] private int _cleanScore;
    public int CleanScore
    {
        get { return _cleanScore; }
        set {
            _cleanScore = value;
            if (_cleanScore < 50)
            {

                fishBowlMat.SetColor("Color_77A2EDE9", pureWaterColor);
            }
            else {
                fishBowlMat.SetColor("Color_77A2EDE9", corruptedWaterColor);
            }
            mossMat.SetFloat("_ShowValue", 1f - CleanScore / 100f + 0.3f);
            Debug.Log("Clean");
        }
    }

    [SerializeField] private int _promotionPoint;
    public int PromotionPoint
    {
        get { return _promotionPoint; }
        set { _promotionPoint = value; }
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
        set { _entrancePercent = value; }
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
    [SerializeField] private Vector3 _floorSize = new Vector3(1,1,1);
    public Vector3 FloorSize
    {
        get { return _floorSize; }
        set { _floorSize = value; }
    }
    #endregion
    [SerializeField] int _promoDispointAmount;
    //�̱���
    //�ؾ��� ��= �̱��� ���ֱ�
    //ũ�� ����
    [SerializeField] GameObject floor;
    [SerializeField] private int _horizontalCount = 5;
    [SerializeField] ParticleSystem _cleaningParticle;
    [SerializeField] ParticleSystem _cleaningParticleSystemObject;
    [SerializeField] GameObject _walls;
    //�����Ƹ��� �� �ü���
    [SerializeField] GameObject _fishBowlObject;
    [SerializeField] GameObject _fishObject;
    [SerializeField] GameObject _decoObject;
    [SerializeField] GameObject _snackShopObject;
    [SerializeField] GameObject _roadTileObject;
    [SerializeField] GameObject _buildPanel;
    public int decoCount = 0;
    public List<GameObject> aquaObject = new List<GameObject>();
    public List<Facility> aquarium = new List<Facility>();
    public List<NavMeshSurface> roadSurfaces;
    public Transform endTarget;


    public LayerMask facilityLayer;
    public Facility facilityObj;

    [SerializeField] private BuildFacility _build;

    public enum STATE
    {
        NORMAL,
        MOVE,
        BUILD
    }
    public STATE state = STATE.NORMAL;
    #region 디버그용
        public void Start()
        {
            InitManager();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L)) Cleaning(10);
            if (Input.GetKeyDown(KeyCode.K)) ChangeSize(0.5f,0.5f);
            if (Input.GetKeyDown(KeyCode.Semicolon)) AddFishBowl();
            if (Input.GetKeyDown(KeyCode.I)) SetPos();
            UpdateManager();
        }
    #endregion
    public void SetFacilityPos()
    {
        if (facilityObj.CheckCollision())
        {
            state = STATE.MOVE;
            Debug.Log(state);

            aquaObject.Add(facilityObj.gameObject);
            if (facilityObj.GetComponent<Fishbowl>())
            {
                aquarium.Add(facilityObj);
            }
            facilityObj = null;
        }
        FindObjectOfType<GridManager>().ShowGrid();

    }
    public void Promotion(int amount)
    {
            PromotionPoint += amount;
    }
    public void ChangeSize(float x, float y)
    {
        _floorSize += new Vector3(x, 1, y);
        floor.transform.localScale = _floorSize;
        floor.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(4 * FloorSize.x, 4 * FloorSize.z);
        
        _walls.transform.GetChild(0).localScale = new Vector3(FloorSize.x*10, 10, 0);
        _walls.transform.GetChild(0).position = new Vector3(0, 5, -FloorSize.z * 10/2);

        _walls.transform.GetChild(1).localScale = new Vector3(FloorSize.x*10, 10, 0);
        _walls.transform.GetChild(1).localRotation = Quaternion.Euler(0, 180, 0);
        _walls.transform.GetChild(1).position = new Vector3(0, 5, FloorSize.z * 10/2);
        
        _walls.transform.GetChild(2).localScale = new Vector3(FloorSize.z*10, 10, 0);
        _walls.transform.GetChild(2).localRotation = Quaternion.Euler(0, 90, 0);
        _walls.transform.GetChild(2).position = new Vector3(FloorSize.x * 10 / 2, 5, 0);
        
        _walls.transform.GetChild(3).localScale = new Vector3(FloorSize.z * 10, 10, 0);
        _walls.transform.GetChild(3).localRotation = Quaternion.Euler(0, -90, 0);
        _walls.transform.GetChild(3).position = new Vector3(-FloorSize.x * 10 / 2, 5, 0);

    }
    public void AddFishBowl()
    {
        //if (_buildPanel) _buildPanel.SetActive(false);
        Fishbowl fishBowl = Instantiate(_fishBowlObject).GetComponent<Fishbowl>();
        fishBowl.transform.localPosition = Vector3.zero;
        facilityObj = fishBowl;

        state = STATE.BUILD;
        FindObjectOfType<GridManager>().ShowGrid();
        //    else floor.transform.localPosition = new Vector3(_floorSize.x / 2+10, 0, 0);
    }
    public void AddSnackShop()
    {
        //_buildPanel.SetActive(false);
        SnackShop snackShop = Instantiate(_snackShopObject).GetComponent<SnackShop>();
        
        snackShop.transform.localPosition = Vector3.zero;
        facilityObj = snackShop;
        state = STATE.BUILD;
        FindObjectOfType<GridManager>().ShowGrid();
    }
    public void AddRoadTile()
    {
        //_buildPanel.SetActive(false);
        RoadTile roadTile = Instantiate(_roadTileObject).GetComponent<RoadTile>();
        
        roadTile.transform.localPosition = Vector3.zero;
        facilityObj = roadTile;
        state = STATE.BUILD;
        roadSurfaces.Add(roadTile.GetComponent<NavMeshSurface>());
        FindObjectOfType<GridManager>().ShowGrid();
    }
    public GameObject AddFish(int id, Transform parent)
    {
        GameObject fish = Instantiate(_fishObject);
        fish.transform.parent = parent;
        fish.transform.localPosition = new Vector3(Random.Range(-0.8f,0.8f), Random.Range(0.2f,1f), Random.Range(-0.8f, 0.8f));
        fish.transform.rotation = Quaternion.Euler(0, Random.Range(0,360), 0);
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

    private void OnTouchHandle(){
    }

    public void InitManager()
    {

        _build = GetComponent<BuildFacility>();
        ChangeSize(0, 0);
        //GameManager.Instance.GetManager<InputManager>().OnMouseClickEvent += MouseClickHandle;
        //GameManager.Instance.GetManager<TimeManager>().OnDayChangedEvent += OnDayChange;
    }

    public void SetPos()
    {
        if(state==STATE.BUILD)
        {
            if (facilityObj && facilityObj.GetComponent<Facility>().CheckCollision())
            {
                facilityObj.transform.parent = facilityParent;
            FindObjectOfType<GridManager>().HideGrid();
                if (facilityObj.GetComponent<NavMeshSurface>())
                {
                    foreach(var s in roadSurfaces)
                    {
                        s.BuildNavMesh();
                    }
                }
                facilityObj = null;
            state = STATE.NORMAL;
                
            }

        };
        
    }

    public void ResetManager()
    {
        GameManager.Instance.GetManager<InputManager>().OnTouchEvent += OnTouchHandle;
        GameManager.Instance.GetManager<InputManager>().OnTouchUpEvent += OnTouchUpHandle;

    }

    private void OnTouchUpHandle()
    {
        if(state==STATE.BUILD)
        {

            RaycastHit hit;
            Ray ray = Define.MainCam.ScreenPointToRay(GameManager.Instance.GetManager<InputManager>().TouchPosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, facilityLayer))
            {
                facilityObj = hit.collider.GetComponent<Facility>();
            }
            else
            {
                facilityObj = null;

            }
        }
    }

    public void UpdateManager()
    {
        EntrancePercent = Mathf.Clamp((float)((float)aquaObject.Count / (float)EntranceFee) * 100f, 0f, 200f);
        Reputation = Mathf.Clamp((EntrancePercent / 100f * CleanScore / 100f * ArtScore / 100f) * 100f + PromotionPoint,0,100);
        ArtScore = Mathf.Clamp(((float)(decoCount / 2) / aquarium.Count) * 100, 0, 100);
        if (state == STATE.BUILD)   
        {
            RaycastHit hit;
            //Ray ray = Define.MainCam.ScreenPointToRay(GameManager.Instance.GetManager<InputManager>().MousePosition);
            //나중에 돌려놔야함
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (facilityObj != null && Physics.Raycast(ray, out hit, Mathf.Infinity, facilityLayer))
            {

                if (_build == null) _build = gameObject.AddComponent<BuildFacility>();
                facilityObj.transform.position = _build.GetFacilityPos() + Vector3.up*2;
            }
        }
    }
    [ContextMenu("Cleaning")]
    public void Cleaning(int value)
    {
        CleanScore -= value;
        if(!_cleaningParticle )
        {
            ParticleSystem particle = Instantiate(_cleaningParticleSystemObject, Camera.main.transform);
            particle.transform.position = Vector3.zero;
            _cleaningParticle = particle;
        }
        _cleaningParticle.transform.localPosition = Vector3.forward;
        _cleaningParticle.transform.localRotation = Quaternion.identity;
        _cleaningParticle.Play();
        
    }
    public void OnDayChange(int year, int month, int day)
    {
        CleanScore = (int)Mathf.Clamp(CleanScore - Reputation * 3, 0, 100);
        int dispointAmount = _promotionPoint;
        if (PromotionPoint > 0) PromotionPoint -=dispointAmount;
    }
    
}