using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private Rigidbody _rigid;
    private float _speed;

    private Vector2 _bound;

    public void SetUp(float speed, float boundFront, float boundBack){
        _rigid = GetComponent<Rigidbody>();
        _speed = speed;
        _rigid.velocity = Vector3.forward * _speed;
        _bound = new Vector2(boundFront, boundBack);
    }

    private void Update()
    {
        if (!(transform.position.z >= _bound.x)) return;
        Vector3 pos = transform.position;
        pos.z = _bound.y;
        transform.position = pos;
    }
}
