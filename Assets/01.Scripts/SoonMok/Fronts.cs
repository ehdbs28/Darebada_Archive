using JetBrains.Annotations;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fronts : MonoBehaviour
{
    [SerializeField]bool isHolding = false;
    public float max;
    public float min;
    public float theta;
    public float angle;
    public float x;
    public float y;
    void LateUpdate()
    {
        if (isHolding)
        {

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            theta = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
            if (theta < min || theta > max) return;
            transform.localPosition = new Vector3(Mathf.Clamp(pos.x, 0, x), Mathf.Clamp(pos.y, 0, y));
            float deg = -(45.0f - theta) * 2.0f;
            transform.eulerAngles = new Vector3(0, 0, deg);

        }
    }
    public void Hold(bool value)
    {
        isHolding = value;
    }
}
