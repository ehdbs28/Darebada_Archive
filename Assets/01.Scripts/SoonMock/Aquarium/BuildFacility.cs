using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildFacility : MonoBehaviour
{
    [SerializeField] private Vector3 _objectPosition;
    [SerializeField] private Camera _mainCam;
    [SerializeField] private LayerMask _floorLayer;
    
    private void Awake()
    {
        _mainCam = Camera.main;
    }
    public Vector3 GetFacilityPos()
    {
        Vector3 point = GameManager.Instance.GetManager<InputManager>().MousePositionToGroundRayPostion;
        _objectPosition = new Vector3(Mathf.RoundToInt(point.x), Mathf.RoundToInt(point.y), Mathf.RoundToInt(point.z));
        return _objectPosition;
    }
}
