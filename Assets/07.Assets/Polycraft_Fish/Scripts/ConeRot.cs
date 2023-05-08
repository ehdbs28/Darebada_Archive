using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeRot : MonoBehaviour {

    public float speed = 2f;
    public float maxRotation = 45f;

    Vector3 curRot;
    //Quaternion curRotation;

    private void Start()
    {
        curRot = transform.rotation.eulerAngles;
        //curRotation = transform.rotation;
    }

    void Update()
    {
        //transform.rotation = Quaternion.Euler(maxRotation * Mathf.Sin(Time.time * speed), 0f, 0f);
        transform.rotation = Quaternion.Euler(maxRotation * Mathf.Sin(Time.time * speed) + curRot.x, maxRotation * Mathf.Cos(Time.time * speed) + curRot.y, 0f);
    }
}
