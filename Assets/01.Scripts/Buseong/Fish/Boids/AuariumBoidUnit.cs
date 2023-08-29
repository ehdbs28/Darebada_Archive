using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuariumBoidUnit : MonoBehaviour
{
    #region Variables & Initializer
    [Header("Info")]
    private AquariumBoids myBoids;
    private List<AuariumBoidUnit> neighbours = new List<AuariumBoidUnit>();

    public Vector3 targetVec;
    private Vector3 egoVector;
    private float speed;

    private float additionalSpeed = 0;
    private bool isEnemy;

    private MeshRenderer myMeshRenderer;
    private TrailRenderer myTrailRenderer;
    [SerializeField] private Color myColor;

    [Header("Neighbour")]
    [SerializeField] private float obstacleDistance;
    [SerializeField] private float FOVAngle = 120;
    [SerializeField] private float maxNeighbourCount = 50;
    [SerializeField] private float neighbourDistance = 10;

    [Header("ETC")]
    [SerializeField] private LayerMask boidUnitLayer;
    [SerializeField] private LayerMask obstacleLayer;

    private Coroutine findNeighbourCoroutine;
    private Coroutine calculateEgoVectorCoroutine;

    private float _radius = 3f;
    private float _maxDistance = 10f;
    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private GameObject _bait;

    private RaycastHit hit;

    [SerializeField]
    public bool IsBite = false;
    [SerializeField]
    public bool IsSensed = false;

    [SerializeField]
    private Vector3 _baitVec;

    [SerializeField]
    private FishSO _unitData;

    private SkinnedMeshRenderer _skinnedMR;

    [SerializeField]
    private Vector3 boundsOffset;

    public void InitializeUnit(AquariumBoids _boids, float _speed, FishSO _fishSO, Vector3 offset)
    {
        myBoids = _boids;
        speed = _speed;
        _unitData = _fishSO;
        boundsOffset = offset;
        gameObject.name = _unitData.Name;
        myTrailRenderer = GetComponentInChildren<TrailRenderer>();
        myMeshRenderer = GetComponentInChildren<MeshRenderer>();

        findNeighbourCoroutine = StartCoroutine("FindNeighbourCoroutine");
        calculateEgoVectorCoroutine = StartCoroutine("CalculateEgoVectorCoroutine");
        _bait = GameObject.Find("Bait");
        _baitVec = _bait.transform.position;
        _skinnedMR = GetComponentInChildren<SkinnedMeshRenderer>();
        _skinnedMR.sharedMesh = _fishSO.Mesh;
    }

    #endregion

    void Update()
    {
        if (additionalSpeed > 0)
            additionalSpeed -= Time.deltaTime;

        //필요한 모든 벡터
        Vector3 cohesionVec = CalculateCohesionVector() * myBoids.cohesionWeight;
        Vector3 alignmentVec = CalculateAlignmentVector() * myBoids.alignmentWeight;
        Vector3 separationVec = CalculateSeparationVector() * myBoids.separationWeight;
        // 추가적인 방향
        Vector3 boundsVec = CalculateBoundsVector() * myBoids.boundsWeight;
        Vector3 obstacleVec = CalculateObstacleVector() * myBoids.obstacleWeight;
        Vector3 egoVec = egoVector * myBoids.egoWeight;

        if (IsSensed)
        {
            myBoids.Fishes.Remove(this.gameObject);
            targetVec = _bait.transform.position - transform.position;
        }
        else
        {
            targetVec = cohesionVec + alignmentVec + separationVec + boundsVec + obstacleVec + egoVec;
        }

        if (!IsBite)
        {
            targetVec = Vector3.Lerp(this.transform.forward, targetVec, Time.deltaTime);
            targetVec = targetVec.normalized;
            if (targetVec == Vector3.zero)
                targetVec = egoVector;

            this.transform.rotation = Quaternion.LookRotation(targetVec);
            this.transform.position += targetVec * (_unitData.Speed + additionalSpeed) * Time.deltaTime;
        }
        else
        {
            this.transform.rotation = Quaternion.LookRotation(targetVec);
            this.transform.position = _bait.transform.position;
        }

    }


    #region Calculate Vectors
    IEnumerator CalculateEgoVectorCoroutine()
    {
        //speed = Random.Range(myBoids.speedRange.x, myBoids.speedRange.y);
        egoVector = Random.insideUnitSphere;
        yield return new WaitForSeconds(Random.Range(1, 3f));
        calculateEgoVectorCoroutine = StartCoroutine("CalculateEgoVectorCoroutine");
    }
    IEnumerator FindNeighbourCoroutine()
    {
        neighbours.Clear();

        Collider[] colls = Physics.OverlapSphere(transform.position, neighbourDistance, boidUnitLayer);
        for (int i = 0; i < colls.Length; i++)
        {
            if (Vector3.Angle(transform.forward, colls[i].transform.position - transform.position) <= FOVAngle && colls[i].name == gameObject.name)
            {
                neighbours.Add(colls[i].GetComponent<AuariumBoidUnit>());
            }
            if (i > maxNeighbourCount)
            {
                break;
            }
        }
        yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        findNeighbourCoroutine = StartCoroutine("FindNeighbourCoroutine");
    }
    private Vector3 CalculateCohesionVector()
    {
        Vector3 cohesionVec = Vector3.zero;
        if (neighbours.Count > 0)
        {
            // 이웃 unit들의 위치 더하기
            for (int i = 0; i < neighbours.Count; i++)
            {
                cohesionVec += neighbours[i].transform.position;
            }
        }
        else
        {
            // 이웃이 없으면 vector3.zero 반환
            return cohesionVec;
        }

        // 중심 위치로의 벡터 찾기
        cohesionVec /= neighbours.Count;
        cohesionVec -= transform.position;
        cohesionVec.Normalize();
        return cohesionVec;
    }

    private Vector3 CalculateAlignmentVector()
    {
        Vector3 alignmentVec = transform.forward;
        if (neighbours.Count > 0)
        {
            // 이웃들이 향하는 방향의 평균 방향으로 이동
            for (int i = 0; i < neighbours.Count; i++)
            {
                alignmentVec += neighbours[i].transform.forward;
            }
        }
        else
        {
            // 이웃이 없으면 그냥 forward로 이동
            return alignmentVec;
        }

        alignmentVec /= neighbours.Count;
        alignmentVec.Normalize();
        return alignmentVec;
    }

    private Vector3 CalculateSeparationVector()
    {
        Vector3 separationVec = Vector3.zero;
        if (neighbours.Count > 0)
        {
            // 이웃들을 피하는 방향으로 이동
            for (int i = 0; i < neighbours.Count; i++)
            {
                separationVec += (transform.position - neighbours[i].transform.position);
            }
        }
        else
        {
            // 이웃이 없으면 vector.zero 반환
            return separationVec;
        }
        separationVec /= neighbours.Count;
        separationVec.Normalize();
        return separationVec;
    }

    private Vector3 CalculateBoundsVector()
    {
        Vector3 offsetToCenter = boundsOffset - transform.position;

        return offsetToCenter.magnitude >= myBoids.spawnRange ? offsetToCenter.normalized : Vector3.zero;
    }

    private Vector3 CalculateObstacleVector()
    {
        Vector3 obstacleVec = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, obstacleDistance, obstacleLayer))
        {
            Debug.DrawLine(transform.position, hit.point, Color.black);
            obstacleVec = hit.normal;
            //additionalSpeed = 10;
        }
        return obstacleVec;
    }
    #endregion


    public void DrawVectorGizmo(int _depth)
    {
        for (int i = 0; i < neighbours.Count; i++)
        {
            if (_depth + 1 < myBoids.GizmoColors.Length - 1)
                neighbours[i].DrawVectorGizmo(_depth + 1);

            Debug.DrawLine(this.transform.position, neighbours[i].transform.position, myBoids.GizmoColors[_depth + 1]);
            Debug.DrawLine(this.transform.position, this.transform.position + targetVec, myBoids.GizmoColors[0]);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hit.point, _radius);
    }
#endif 
}