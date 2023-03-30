using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceUnit : MonoBehaviour
{
    public float Weight;

    private LayerMask _weightObjLayer;

    private Transform _rayStartPos;

    private Collider[] hit;

    private float _initWeight;
    private float _heavyWeight;

    private float _height;

    private void Awake()
    {
        _weightObjLayer = LayerMask.GetMask("Heavy_weight_object");
        _rayStartPos = transform.Find("RayStartPos");
        _initWeight = Weight;
        _heavyWeight = _initWeight + 5;
        _height = transform.parent.localScale.y;
    }

    public void CheckAddWeight(bool isHeavyUnit = false)
    {
        hit = Physics.OverlapBox(_rayStartPos.position, new Vector3((transform.lossyScale * .5f).x, 0.1f, (transform.lossyScale * .5f).z), transform.rotation, _weightObjLayer);
        if (!isHeavyUnit)
        {
            if (hit.Length > 0)
            {
                Weight = _heavyWeight;
            }
            else
            {
                Weight = _initWeight;
            }
        }
    }

    public void ValueUP()
    {
        _height = Mathf.Lerp(_height, 1f, Time.deltaTime);
        if (_height >= 1f)
            return;
        transform.parent.localScale = new Vector3(1, _height, 1);
    }

    public void ValueDown()
    {
        _height = Mathf.Lerp(_height, 0.1f, Time.deltaTime);
        if (_height <= 0.1f)
            return;
        transform.parent.localScale = new Vector3(1, _height, 1);
    }
}
