using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildFacility : MonoBehaviour
{
    [SerializeField] private Vector3 _objectPosition;
    [SerializeField] private Vector3 _beforeVector; 
    public Vector3 GetFacilityPos()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition);
        Physics.Raycast( ray, out hit,Mathf.Infinity,GetComponent<AquariumManager>().facilityLayer);
        if (hit.collider)
        {
            _objectPosition = hit.collider.transform.position;
            Debug.Log(_objectPosition);

        }
        else
        {
            _objectPosition = _beforeVector;
            Debug.Log(hit.point);
        }
        return _objectPosition;
    }
}
