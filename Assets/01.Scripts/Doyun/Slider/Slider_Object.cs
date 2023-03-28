using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider_Object : MonoBehaviour
{
    [Header("슬라이더 값")]
    [Range(0, 1)][SerializeField] private float _value = 0f;

    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _endPos;
    [SerializeField] private Transform _handle;

    private bool _isDragHandle = false;
    private Vector3 _dragStartPos;

    public float Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            _handle.position = Vector3.Lerp(_startPos.position, _endPos.position, _value);
        }
    }

    private void Update()
    {
        Value = _value;
        MouseMovement();
    }

    private void MouseMovement()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            _isDragHandle = Physics.Raycast(ray, out hit, Camera.main.farClipPlane, LayerMask.GetMask("Slider_Handle"));
            
            if(_isDragHandle)
                _dragStartPos = hit.transform.position;
        }
        else if (Input.GetMouseButton(0) && _isDragHandle)
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, transform.position.y, transform.position.z);
            Debug.Log(mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragHandle = false;
        }
    }
}
