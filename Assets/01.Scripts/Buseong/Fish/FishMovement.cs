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

    private void Update()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 2f, transform.forward, out hit, _rayMaxDistance))
            _canMoveForward = false;
        else
        {
            _canMoveForward = true;
        }

        if (_canMoveForward)
        {
            FishMove();
        }
        else
        {
            SetDirection();
            FishMove();
        }
    }

    private void FishMove()
    {
        _rigidbody.velocity = _dir * Time.deltaTime * _fish.SwimSpeed;
    }

    private void SetDirection()
    {
        _dir += new Vector3(0f, _dir.y + 1f, 0f);
    }
}
