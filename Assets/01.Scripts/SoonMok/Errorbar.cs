using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Errorbar : MonoBehaviour, IsClickObj
{
    [SerializeField]private bool isHold = false;
    private Vector3 startPoint;
    public void OnClick()
    {
        SetHold(true);
        startPoint = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPoint.z = 0;
    }

    public void OnDrag()
    {
        
    }

    public void OnDragEnd()
    {
        SetHold(false);
    }

    public void SetHold(bool value)
    {
        isHold = value;
    }
    void Update()
    {
        if (isHold)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            transform.parent.position = startPoint +pos;
        }
    }
}
