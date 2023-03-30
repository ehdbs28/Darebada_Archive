using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLock : MonoBehaviour
{
    private Bolt[] _bolts;

    private void Awake()
    {
        _bolts = transform.GetComponentsInChildren<Bolt>();
    }

    private void Update()
    {
        if (CheckOpen())
        {
            gameObject.SetActive(false);
        }
    }

    bool CheckOpen()
    {
        foreach(var bolt in _bolts)
        {
            if (bolt.gameObject.activeSelf == true)
                return false;
        }

        return true;
    }
}
