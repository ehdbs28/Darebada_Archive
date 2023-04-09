using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockObj : MonoBehaviour
{
    private float _doorObjPosX;
    private Transform _doorObjTransform;
    public bool isLocked = true;
    public MeshRenderer meshRenderer;
    [SerializeField]
    float targetX;

    void Start()
    {
        _doorObjTransform = GetComponent<Transform>();
        _doorObjPosX = transform.position.x;
        meshRenderer = GetComponent<MeshRenderer>();
        targetX = transform.position.x + transform.localScale.x;

    }

    void Update()
    {
        CheckKey();
    }

    private void CheckKey()
    {
        if (!isLocked)
        {
            _doorObjPosX = Mathf.Lerp(_doorObjPosX, targetX, Time.deltaTime);
            if (_doorObjPosX >= targetX)
                return;
            transform.position = new Vector3(_doorObjPosX, transform.position.y, transform.position.z);
        }
    }
}
