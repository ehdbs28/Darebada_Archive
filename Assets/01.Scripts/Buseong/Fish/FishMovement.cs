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

    [SerializeField]
    private float _swimSpeed;

    private float timeTest;
    private float timeTest2 = 3f;

    float additionalSpeed = 0;

    private void Update()
    {
        RaycastHit hit;
        Physics.SphereCast(transform.position, _radius, transform.forward, out hit, _maxDistance, _layerMask);
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