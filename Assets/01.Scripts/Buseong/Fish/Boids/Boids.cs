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
    private List<BoidUnit> _boidUnitPrefabList;
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
    private FishSO _fishData;

    public bool IsBite = true;
    public GameObject Bait;
    public GameObject BaitSenseArea;

    public GameObject NonSpawnArea;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Bait = GameObject.Find("Bait");
        BaitSenseArea = GameObject.Find("BaseBait");
        NonSpawnArea = GameObject.Find("Non Spawn Area");
        boundMR = GetComponentInChildren<MeshRenderer>();
        for (int i = 0; i < boidCount; i++)
        {
            Vector3 randomVec = Random.insideUnitSphere - NonSpawnArea.transform.position;
            randomVec *= spawnRange;
            Quaternion randomRot = Quaternion.Euler(0, Random.Range(0, 360f), 0);
            BoidUnit currUnit = Instantiate(boidUnitPrefab, this.transform.position + randomVec, randomRot);
            currUnit.transform.SetParent(this.transform);
            currUnit.InitializeUnit(this, Random.Range(speedRange.x, speedRange.y), i, _fishData);
        }
        IsBite = false;
    }
    #endregion
}