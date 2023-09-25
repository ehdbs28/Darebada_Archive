using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private BIOME _habitatBiome;
    private float _habitatX; //서식지X값
    private float _habitatY; //서식지Y값
    private FISHSPECIES _fishSpecies;
    [SerializeField]private float _swimSpeed; //헤엄 속도
    public float SwimSpeed => _swimSpeed;
    private float _turnSpeed; //회전 속도
    private float _rarity;    
    private float _level;
    private float _cost;
    private float _size;
    private float _spawnPercent;
    private float additionalSpeed = 10f;
    [SerializeField] private float obstacleDistance;

    private List<Fish> fishes;

    [SerializeField]
    private Collider[] cols;

    public void Init(FishSO data)
    {
        //_habitatBiome = data.HabitatBiome;
        //_habitatX = data.HabitatX;
        //_habitatY = data.HabitatY;
        //_fishSpecies = data.FishSpecies;
        //_swimSpeed = data.SwimSpeed;
        //_turnSpeed = data.TurnSpeed;
        //_rarity = data.Rarity;
        //_level = data.Level;
        //_cost = data.Cost;
        //_size = data.Size;
        //_spawnPercent = data.SpawnPercent;
    }

    //public void Awake()
    //{
    //}
    //public void SetTargets()
    //{

    //    fishes = new List<Fish>();
    //    cols = Physics.OverlapSphere(transform.position, 10f);
    //    foreach(Collider col in cols)
    //    {
    //        fishes.Add(col.GetComponent<Fish>());
    //    }
    //}
    //[SerializeField] float _cohPow;
    //[SerializeField] float _aliPow;
    //[SerializeField] float _sepPow;
    //public Vector3 ctrl;
    //public void Update()
    //{
    //    SetTargets();
    //    Vector3 dir =Vector3.Lerp((transform.forward + ctrl.normalized).normalized, 
    //        Cohesion()*_cohPow+ Alignment()*_aliPow + Separation() * 
    //        _sepPow,Time.deltaTime).normalized;
    //    transform.position += dir * _swimSpeed * Time.deltaTime;
        
    //    transform.rotation = Quaternion.LookRotation(dir);

    //}
    //Vector3 Cohesion()
    //{
    //    Vector3 cohesionVec = Vector3.zero;
    //    if (fishes.Count > 0)
    //    {
    //        // 이웃 unit들의 위치 더하기
    //        for (int i = 0; i < fishes.Count; i++)
    //        {
    //            cohesionVec += fishes[i].transform.position;
    //        }
    //    }
    //    else
    //    {
    //        // 이웃이 없으면 vector3.zero 반환
    //        return cohesionVec;
    //    }

    //    // 중심 위치로의 벡터 찾기
    //    cohesionVec /= fishes.Count;
    //    cohesionVec -= transform.position;
    //    cohesionVec.Normalize();
    //    return cohesionVec;
    //}
    //Vector3 Alignment()
    //{
    //    Vector3 alignmentVec = transform.forward;
    //    if (fishes.Count > 0)
    //    {
    //        // 이웃들이 향하는 방향의 평균 방향으로 이동
    //        for (int i = 0; i < fishes.Count; i++)
    //        {
    //            alignmentVec += fishes[i].transform.forward;
    //        }
    //    }
    //    else
    //    {
    //        // 이웃이 없으면 그냥 forward로 이동
    //        return alignmentVec;
    //    }

    //    alignmentVec /= fishes.Count;
    //    alignmentVec.Normalize();
    //    return alignmentVec;
    //}
    //Vector3 Separation()
    //{
    //    Vector3 separationVec = Vector3.zero;
    //    if (fishes.Count > 0)
    //    {
    //        // 이웃들을 피하는 방향으로 이동
    //        for (int i = 0; i < fishes.Count; i++)
    //        {
    //            separationVec += (transform.position - fishes[i].transform.position);
    //        }
    //    }
    //    else
    //    {
    //        // 이웃이 없으면 vector.zero 반환
    //        return separationVec;
    //    }
    //    separationVec /= fishes.Count;
    //    return separationVec;

    //}

    //private Vector3 CalculateObstacleVector()
    //{
    //    Vector3 obstacleVec = Vector3.zero;
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, transform.forward, out hit, obstacleDistance))
    //    {
    //        Debug.DrawLine(transform.position, hit.point, Color.black);
    //        obstacleVec = hit.normal;
    //        additionalSpeed = 10;
    //    }
    //    return obstacleVec;
    //}
}
