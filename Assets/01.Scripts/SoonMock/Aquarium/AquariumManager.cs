using Core;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AquariumManager : MonoBehaviour
{
    static private AquariumManager _instance;
    static public AquariumManager Instance
    {
        get
        {
            if (_instance == null)
            {
                if(FindObjectOfType<AquariumManager>()) _instance = FindObjectOfType<AquariumManager>(); 
                else _instance = new AquariumManager();
            }
            return _instance;
        }
        private set { _instance = value; }
    }
    public Transform facilityParent;
    public List<GameObject> fishBowls = new List<GameObject>();
    public List<DecoVisualSO> decoVisuals = new List<DecoVisualSO>();
    public enum STATE
    {
        NORMAL,//플레이어 조작
        CAMERAMOVE,//카메라이동. 눌러서 이동한다
        BUILD//아쿠아리움 오브젝트를 설치한다.
    }
    public STATE state = STATE.NORMAL;
    [SerializeField] GameObject floor;
    [SerializeField] GameObject _walls;
    [SerializeField] GameObject _fishBowlObject;
    [SerializeField] GameObject _snackShopObject;
    [SerializeField] GameObject _roadTileObject;
    [SerializeField] ParticleSystem _cleaningParticle;
    [SerializeField] ParticleSystem _cleaningParticleSystemObject;
    public List<NavMeshSurface> roadSurfaces;

    [Header("LayerMask")]
    public LayerMask facilityLayer;
    public LayerMask gridLayer;

    public Facility facilityObj;

    [SerializeField] private BuildFacility _build;

        public void Start()
        {
            InitManager();
        }
        private void Update()
        {
            UpdateManager();
        }
    public void SetFacilityPos()
    {
        if (facilityObj.CheckCollision())
        {
            
            if (facilityObj.GetComponent<Fishbowl>())
            {
                GameManager.Instance.GetManager<AquariumNumericalManager>().fishbowlCnt++;
            }
            facilityObj = null;
            state = STATE.CAMERAMOVE;
        }
        FindObjectOfType<GridManager>().ShowGrid();

    }   
    public void Promotion(int amount)
    {
        GameManager.Instance.GetManager<AquariumNumericalManager>().PromotionPoint += amount;
    }
    public void ChangeSize(float x, float y)
    {
        GameManager.Instance.GetManager<AquariumNumericalManager>().FloorSize += new Vector3(x, 1, y);
        Debug.Log(floor);
        floor.transform.localScale = GameManager.Instance.GetManager<AquariumNumericalManager>().FloorSize;
        Vector3 FloorSize = GameManager.Instance.GetManager<AquariumNumericalManager>().FloorSize;
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
        RoadTile roadTile = Instantiate(_roadTileObject).GetComponent<RoadTile>();

        GameManager.Instance.GetManager<AquariumNumericalManager>().roadCnt++;
        
        roadTile.transform.localPosition = Vector3.zero;
        facilityObj = roadTile;
        state = STATE.BUILD;
        roadSurfaces.Add(roadTile.GetComponent<NavMeshSurface>());
        FindObjectOfType<GridManager>().ShowGrid();
    }

    private void OnTouchHandle(){
    }

    public void GenerateCustomer()
    {
        if (GameManager.Instance.GetManager<AquariumNumericalManager>().roadCnt <= 0)
            return;

        for (int i = 0; i < GameManager.Instance.GetManager<AquariumNumericalManager>().CustomerCnt; i++)
        {
            int type = Random.Range(1, 4);
            CustomerController customer = GameManager.Instance.GetManager<PoolManager>().Pop($"Customer{type}") as CustomerController;
        }
    }

    public void InitManager()
    {

        _build = GetComponent<BuildFacility>();
        Define.MainCam.clearFlags = CameraClearFlags.SolidColor;
        Define.MainCam.backgroundColor = Color.black;
        ChangeSize(0, 0);
        ResetManager();
    }
    public void ReleaseManager()
    {
        Define.MainCam.clearFlags = CameraClearFlags.Skybox;
    }
    public void SetPos()
    {
        if(state==STATE.BUILD)
        {
            if (facilityObj && facilityObj.GetComponent<Facility>().CheckCollision())
            {
                FindObjectOfType<GridManager>().HideGrid();
                if(facilityObj.GetComponent<Fishbowl>())
                {
                    foreach(var v in facilityObj.GetComponent<Fishbowl>().boids)
                    {
                        v.Value.SetMove(true);
                    }
                }
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
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, gridLayer))
            {
                SetPos();
            }
            else
            {
            }
        }
    }

    public void UpdateManager()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            FindObjectOfType<Fishbowl>().Upgrade();
        }
        if (state == STATE.BUILD)   
        {
            RaycastHit hit;
            Ray ray = Define.MainCam.ScreenPointToRay(GameManager.Instance.GetManager<InputManager>().TouchPosition);
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (facilityObj != null && Physics.Raycast(ray, out hit, Mathf.Infinity, gridLayer))
            {

                if (_build == null) _build = gameObject.AddComponent<BuildFacility>();
                facilityObj.transform.position = _build.GetFacilityPos() + Vector3.up*2;
            }
        }
    }

    public void Cleaning(int value)
    {
        GameManager.Instance.GetManager<AquariumNumericalManager>().CleanScore -= value;
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
    
}