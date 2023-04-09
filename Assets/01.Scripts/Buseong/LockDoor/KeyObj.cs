using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObj : MonoBehaviour
{
    private Collider[] hit;
    [SerializeField] LayerMask _lockedDoorLayer;

    private void Update()
    {
        CheckUseKey();
    }

    private void CheckUseKey()
    {
        hit = Physics.OverlapBox(transform.position, transform.localScale, transform.rotation, _lockedDoorLayer);
        if(hit.Length > 0)
        {
            GameObject.Find(hit[0].name).GetComponent<LockObj>().isLocked = false;
        }
    }
}
