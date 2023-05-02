using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    [SerializeField]
    private Vector3[] _positions;
    [SerializeField]
    private GameObject _endPos;
    [SerializeField]
    private float maxDistance = 20f;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DrawLaser();
    }

    private void DrawLaser()
    {
        _positions[0] = transform.position;
        _positions[1] = _endPos.transform.position;
        _lineRenderer.enabled = true;
        _lineRenderer.SetPositions(_positions);
        
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            Destroy(hit.collider.gameObject);
        }
    }
}
