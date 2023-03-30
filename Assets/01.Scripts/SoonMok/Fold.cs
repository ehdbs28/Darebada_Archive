using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fold : MonoBehaviour
{
    public Vector3 target;
    public bool isMoving = false;
    public bool isUp = true;
    private void Update()
    {
        
        if(isMoving)
        {
            transform.localPosition += (target - transform.localPosition)* Time.deltaTime;
            if ((transform.localPosition.y >= target.y + transform.parent.localPosition.y - 0.005f && isUp)||(transform.localPosition.y <= target.y + transform.parent.position.y+0.005f && !isUp))
            {
                isMoving = false;
                target = Vector3.zero;
            }
        }
    }
}
