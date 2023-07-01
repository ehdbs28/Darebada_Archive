using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildFacility : MonoBehaviour
{
    [SerializeField] private Vector3 _objectPosition;
    
    public Vector3 GetFacilityPos()
    {
        Vector3 point = GameManager.Instance.GetManager<InputManager>().GetMouseRayPoint();
        _objectPosition = new Vector3(Mathf.RoundToInt(point.x), Mathf.RoundToInt(point.y), Mathf.RoundToInt(point.z));
        return _objectPosition;
    }
}
