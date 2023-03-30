using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider_Object : MonoBehaviour
{
    [Range(0, 1)][SerializeField] private float _value = 0f;

    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _endPos;
    [SerializeField] private Transform _handle;

    private bool _isDragHandle = false;

    private GameObject _dragHandle;
    private Vector3 _dragStartPos;
    private Vector3 _dragStartRayPos;
    private float _startValue = 0;

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
        Debug.DrawRay(ray.origin, ray.direction * 100);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            _isDragHandle = Physics.Raycast(ray, out hit, Camera.main.farClipPlane, LayerMask.GetMask("Slider_Handle"));
            if (_isDragHandle)
            {
                _dragHandle = hit.transform.gameObject;
                _dragStartPos = hit.transform.localPosition;
                _dragStartRayPos = ray.origin;
                _startValue = Value;
            }
        }
        else if (Input.GetMouseButton(0) && _isDragHandle)
        {
            Vector3 rayPoint = ray.GetPoint(Vector3.Distance(_dragStartRayPos, _dragStartPos));
            if (Vector3.Cross(rayPoint - _dragStartPos, _dragHandle.transform.up).z > 0)
            {
                //오른쪽으로 가는 경우
                Debug.Log(1);
                //float targetValue = _startValue - Mathf.Abs(_dragStartPos.x - rayPoint.x) / Mathf.Abs((_endPos.localPosition.x - _startPos.localPosition.x));
                float targetValue = _startValue + Mathf.Abs(rayPoint.x - _dragStartPos.x) / Mathf.Abs((_endPos.localPosition.x - _startPos.localPosition.x));
                Value = Mathf.Clamp(targetValue, 0f, 1f);
            }
            else
            {
                //왼쪽으로 가는 경우
                Debug.Log(2);
                //float targetValue = _startValue + Mathf.Abs(_dragStartPos.x - rayPoint.x) / Mathf.Abs((_endPos.localPosition.x - _startPos.localPosition.x));
                float targetValue = _startValue - Mathf.Abs(rayPoint.x - _dragStartPos.x) / Mathf.Abs((_endPos.localPosition.x - _startPos.localPosition.x));
                Value = Mathf.Clamp(targetValue, 0f, 1f);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragHandle = false;
        }
    }
}
