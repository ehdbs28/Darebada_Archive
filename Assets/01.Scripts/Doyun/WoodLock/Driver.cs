using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour, IsClickObj
{
    [SerializeField] private LayerMask _targetLayer;
    private bool _isHover = false;
    private Vector3 distance;

    public void OnClick()
    {
        _isHover = true;
    }

    public void OnDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);
        if (_isHover)
        {

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _targetLayer))
            {
                if (distance == Vector3.zero) { distance = transform.position - hit.point; }
                Debug.Log(hit.point + distance);
                transform.position = hit.point + distance;
            }

        }
    }

    public void OnDragEnd()
    {
        _isHover = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bolt"))
        {
            other.GetComponent<Bolt>().OnDrived();
        }
    }
}
