using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Boids : MonoBehaviour
{
    public static Boids Instance;

    #region Variables & Initializer
    [Header("Boid Options")]
    [SerializeField] private BoidUnit boidUnitPrefab;
    [SerializeField]
    private FishDataTable _unitDataTable;
    [Range(5, 5000)]
    public int boidCount;
    
    public float spawnRange = 30;
    public Vector2 speedRange;

    [Range(0, 10)]
    public float cohesionWeight = 1;
    [Range(0, 10)]
    public float alignmentWeight = 1;
    [Range(0, 10)]
    public float separationWeight = 1;

    [Range(0, 100)]
    public float boundsWeight = 1;
    [Range(0, 100)]
    public float obstacleWeight = 10;
    [Range(0, 10)]
    public float egoWeight = 1;


    public BoidUnit currUnit;
    private MeshRenderer boundMR;
    [SerializeField] LayerMask unitLayer;

    [Header("Camera")]
    [SerializeField] CinemachineVirtualCamera originCam;
    [SerializeField] CinemachineFreeLook unitCam;

    [Header("OPTIONAL")]
    public bool ShowBounds = false;
    public bool cameraFollowUnit = false;
    public bool randomColor = false;
    public bool blackAndWhite = false;
    public bool protectiveColor = false;
    public float enemyPercentage = 0.01f;
    public Color[] GizmoColors;

    [SerializeField]
    private FishDataUnit _fishData;

    public bool IsBite = true;
    public GameObject Bait;

    public GameObject NonSpawnArea;

    private float _total = 0f;

    private OceneType _currentBiome;

    public List<GameObject> Fishes;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    public void Setup(OceneType type, FishDataTable dataTable)
    {
        _currentBiome = type;
        for (int i = 0; i < dataTable.Size; i++)
        {
            if (dataTable.DataTable[i].Habitat == type)
            {
                _unitDataTable.DataTable.Add(dataTable.DataTable[i]);
            }
        }

        Fishes = new List<GameObject>();
        Bait = GameObject.Find("Bait");
        //NonSpawnArea = GameObject.Find("Non Spawn Area");
        boundMR = GetComponentInChildren<MeshRenderer>();

        for (int i = 0; i < _unitDataTable.Size; i++)
        {
            _total += _unitDataTable.DataTable[i].SpawnPercent;
        }

        for (int i = 0; i < boidCount; i++)
        {
            SpawnFish();
        }
        IsBite = false;
    }

    private void Update()
    {
        Debug.Log($"현재 물고기 수: {Fishes.Count}");
        if(Fishes.Count != boidCount)
        {
            SpawnFish();
        }
    }

    private void SpawnFish()
    {
        float randomPoint = Random.value * _total;
        Debug.Log($"현재 랜덤 값: {randomPoint}");
        for (int j = 0; j < _unitDataTable.Size; j++)
        {
            if (randomPoint <= _unitDataTable.DataTable[j].SpawnPercent)
            {
                _fishData = _unitDataTable.DataTable[j];
                break;
            }
            else
            {
                randomPoint -= _unitDataTable.DataTable[j].SpawnPercent;
            }
        }
        Vector3 randomVec = Random.insideUnitSphere;
        //Vector3 randomVec = Random.insideUnitSphere - NonSpawnArea.transform.position;
        randomVec *= spawnRange;
        randomVec.y -= spawnRange * (_fishData.Rarity - 1);
        Quaternion randomRot = Quaternion.Euler(0, Random.Range(0, 360f), 0);
        BoidUnit currUnit = Instantiate(boidUnitPrefab, this.transform.position + randomVec, randomRot);
        currUnit.transform.SetParent(this.transform);
        Vector3 offset = new Vector3(0f, randomVec.y, 0f);
        currUnit.InitializeUnit(this, _fishData.Speed, _fishData, offset);
        Fishes.Add(currUnit.gameObject);
    }

    #endregion
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        ////Gizmos.DrawWireSphere(transform.position, spawnRange);
        //Gizmos.DrawWireSphere(offset.transform.position, spawnRange);
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(offset2.transform.position, spawnRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, spawnRange  * 0, 0), spawnRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, spawnRange * 1, 0), spawnRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, spawnRange * 2, 0), spawnRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, spawnRange * 3, 0), spawnRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, spawnRange * 4, 0), spawnRange);
    }
#endif
}