using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FishMovement : MonoBehaviour
{
    private Fish _fish;
    private Rigidbody _rigidbody;

    private bool _canMoveForward = true;
    private float _rayMaxDistance = 5f;

    private Vector3 _dir;

    private float _timer;
    private float _setTime = 3f;

    private float _xPosMinLimit = -30f;
    private float _xPosMaxLimit = 30f;
    private float _yPosMinLimit = -30f;
    private float _yPosMaxLimit = 30f;
    private float _zPosMinLimit = -30f;
    private float _zPosMaxLimit = 30f;

    public bool IsSelected { get; set; }
    public bool IsCatched { get; private set; }

    public Transform Target { get; set; }

    private void Start()
    {
        _fish = GetComponent<Fish>();
        _rigidbody = GetComponent<Rigidbody>();

        _dir = SetRandomDirection(_dir);
    }

    private void Update()
    {
        if(IsSelected == false){
            _timer += Time.deltaTime;
            if (_setTime < _timer)
            {
                _setTime = _timer + 3f;
                _dir = SetRandomDirection(_dir);
                
                if((transform.position.x > _xPosMaxLimit || transform.position.x < _xPosMinLimit)
                    ||(transform.position.y > _yPosMaxLimit || transform.position.y < _yPosMinLimit)
                    ||(transform.position.z > _zPosMaxLimit || transform.position.z < _zPosMinLimit))
                {
                    _dir = GameObject.Find("FishManager").transform.position - transform.position;
                }

                Debug.Log(_dir);
            }
            //_rigidbody.velocity = _dir * _fish.SwimSpeed;
        }
        else{
            if(Vector3.Distance(Target.position, transform.position) <= 0.1f){
                IsCatched = true;
            }
            else{
                IsCatched = false;
            }

            _dir = (Target.position - transform.position).normalized;
        }

        if(IsCatched == false){
            _rigidbody.AddForce(_dir.normalized * _fish.SwimSpeed, ForceMode.Impulse);
        }
        else{
            transform.position = Target.position;
        }
    }

    private Vector3 SetRandomDirection(Vector3 direction)
    {
        //float randomX = 0f;
        //float randomY = 0f;
        //direction.x = Mathf.Clamp(randomX, -45f, 25f);
        //direction.y = Mathf.Clamp(randomY, direction.y - 90f, direction.y + 90f);

        direction.x = Random.Range(-45f, 25f);
        direction.y = Random.Range(direction.y - 90f, direction.y + 90f);

        return direction.normalized;
    }

    private void FixedUpdate()
    {
        
        
    }
}
