using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishMovementModule : CommonModule<OceanFishController>
{
    private Rigidbody _rigid;

    [SerializeField]
    private float _acceralation;
    
    [SerializeField]
    private float _deceralation;
    
    private bool _isMovement;
    public bool IsMovement => _isMovement;

    private const float MaxMoveDis = 30f;

    private Vector3 _dir;
    private Vector3 _target;

    [SerializeField] 
    private BoxCollider _bound;

    private float _currentVelocity;

    public override void SetUp(Transform rootTrm)
    {
        base.SetUp(rootTrm);
        _rigid = rootTrm.GetComponent<Rigidbody>();
        
        _isMovement = true;
        _target = transform.position;
        _dir = Vector3.one;
    }

    public override void UpdateModule()
    {
        CalcVelocity();

        if (!_isMovement)
            return;
        
        if (!CanMovementCheck())
        {
            StartCoroutine(TurnRoutine());
        }
    }

    public override void FixedUpdateModule()
    {
        _rigid.velocity = _dir.normalized * _currentVelocity;
    }

    private IEnumerator TurnRoutine()
    {
        _isMovement = false;
        CalcDir();

        float timer = Random.Range(0.5f, 2f);
        float cur = 0f;
        float percent = 0;

        Quaternion rot = _controller.transform.rotation;

        while (percent <= 1f)
        {
            cur += Time.deltaTime;
            percent = cur / timer;
            _controller.transform.rotation = Quaternion.Slerp(rot, Quaternion.LookRotation(_dir), percent);
            yield return null;
        }

        _isMovement = true;
    }

    private bool CanMovementCheck()
    {
        return Vector3.Distance(transform.position, _target) > 0.5f;
    }

    private void CalcDir()
    {
        // float x = Random.Range(0f, _dir.x > 0 ? -1f : _dir.x == 0 ? 0f : 1f);
        // float y = Random.Range(0f, _dir.y > 0 ? -1f : _dir.y == 0 ? 0f : 1f);
        // float z = Random.Range(0f, _dir.z > 0 ? -1f : _dir.z == 0 ? 0f : 1f);

        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        
        _dir = new Vector3(x, y, z);
        Debug.Log(_dir);
        float dis = Random.Range(5f, MaxMoveDis);
        _target = transform.position + _dir * dis;

        if (!_bound.bounds.Contains(_target))
        {
            _target = _bound.bounds.ClosestPoint(_target);
            _dir = (_target - transform.position).normalized;
        }
    }

    private void CalcVelocity()
    {
        if (_isMovement)
        {
            _currentVelocity += _acceralation * Time.deltaTime;
        }
        else
        {
            _currentVelocity -= _deceralation * Time.deltaTime;
        }

        _currentVelocity = Mathf.Clamp(_currentVelocity, 0f, _controller.DataUnit.Speed);
    }
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!_isMovement) return;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + _dir, 2f);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_target, 3f);
    }
    #endif
}
