using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using JetBrains.Annotations;
using System.Linq;

public class AquariumBoids : MonoBehaviour
{
    #region Variables & Initializer
    [Header("Boid Options")]
    public AuariumBoidUnit boidUnitPrefab;
    [Range(1, 15)]
    public int boidCount;
    [Range(0.00001f, 3f)]
    public float spawnRange = 0.0001f;
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
    public float obstacleWeight = 50;
    [Range(0, 10)]
    public float egoWeight = 1;


    public BoidUnit currUnit;
    private MeshRenderer boundMR;
    public FishDataUnit FishData
    {
        get => _fishData;
        set=>_fishData= value;
    }
    [SerializeField] LayerMask unitLayer;

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
    public List<GameObject> Fishes = new List<GameObject>();

    public void SetPosZero()
    {
        foreach (GameObject obj in Fishes)
        {
            obj.transform.position = Vector3.zero;
        }
    }
    public void GenerateBoids()
    {
        // Generate Boids
        boundMR = GetComponentInChildren<MeshRenderer>();
        Vector3 randomVec = Random.insideUnitSphere;
        randomVec *= spawnRange;
        Quaternion randomRot = Quaternion.Euler(0, Random.Range(0, 360f), 0);
        AuariumBoidUnit currUnit = Instantiate(boidUnitPrefab, this.transform.position + randomVec, randomRot);
        currUnit.transform.SetParent(this.transform);
        currUnit.transform.localPosition = this.transform.localPosition + randomVec;
        currUnit.InitializeUnit(this, Random.Range(speedRange.x, speedRange.y), _fishData, transform.position);
        Fishes.Add(currUnit.gameObject);
        currUnit.IsMove =true;
    }
    public void SetMove(bool val)
    {
        List<AuariumBoidUnit> fishs = GetComponentsInChildren<AuariumBoidUnit>().ToList<AuariumBoidUnit>();
        for(int i = 0; i < fishs.Count; i++)
        {
            fishs[i].IsMove = val;
        }
    }
    #endregion

    
}