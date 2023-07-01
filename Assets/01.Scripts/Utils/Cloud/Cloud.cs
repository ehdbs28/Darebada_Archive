using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private Rigidbody _rigid;
    private float _speed;

    private Vector2 _bound;

    public void SetUp(float speed, float bound_front, float bound_back){
        _rigid = GetComponent<Rigidbody>();
        _speed = speed;
        _rigid.velocity = Vector3.forward * _speed;
        _bound = new Vector2(bound_front, bound_back);
    }

    private void Update() {
        if(transform.position.z >= _bound.x){
            Vector3 pos = transform.position;
            pos.z = _bound.y;
            transform.position = pos;
        }
    }
}
