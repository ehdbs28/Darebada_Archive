using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FishMovement : MonoBehaviour
{
    private float _radius = 5f;
    private float _maxDistance = 50f;
    [SerializeField]
    private LayerMask _layerMask;
    private Vector3 _direction;

    private Fish _fish;
    [SerializeField]
    private float _speed;
    Coroutine calculateEgoVectorCoroutine;
    Vector3 egoVector;
    Vector3 targetVec;


    private float timeTest;
    private float timeTest2 = 3f;

    float additionalSpeed = 0;

    public void Awake()
    {
        _fish = GetComponent<Fish>();

        calculateEgoVectorCoroutine = StartCoroutine("CalculateEgoVectorCoroutine");
    }

    private void Update()
    {
        if (additionalSpeed > 0)
            additionalSpeed -= Time.deltaTime;

        RaycastHit hit;
        Physics.SphereCast(transform.position, _radius, transform.forward, out hit, _maxDistance, _layerMask);

        targetVec = Vector3.Lerp(transform.forward, targetVec, Time.deltaTime);
        targetVec = targetVec.normalized;
        targetVec = egoVector;
        transform.rotation = Quaternion.LookRotation(targetVec);
        transform.position += targetVec * (_speed + additionalSpeed) * Time.deltaTime;
    }

    IEnumerator CalculateEgoVectorCoroutine()
    {
        _speed = Random.Range(1, 5);
        egoVector = Random.insideUnitSphere;
        yield return new WaitForSeconds(Random.Range(1, 3f));
        calculateEgoVectorCoroutine = StartCoroutine("CalculateEgoVectorCoroutine");
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _maxDistance);
            Gizmos.color = Color.white;
        }
    }
#endif
}