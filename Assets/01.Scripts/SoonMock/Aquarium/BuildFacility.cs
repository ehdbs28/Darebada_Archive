using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildFacility : MonoBehaviour
{
    [SerializeField] private Vector3 _objectPosition;
    
    public Vector3 GetFacilityPos()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition);
        Physics.Raycast( ray, out hit,Mathf.Infinity,GetComponent<AquariumManager>().facilityLayer);
        Vector3 point = hit.point;
       // Vector3 point = GameManager.Instance.GetManager<InputManager>().GetMouseRayPoint();
        _objectPosition = new Vector3(Mathf.RoundToInt(point.x)/ 4 * 4, Mathf.RoundToInt(point.y) / 4 * 4, Mathf.RoundToInt(point.z) / 4 * 4);
        return _objectPosition;
    }
}
