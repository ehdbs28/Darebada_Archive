using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZIpperResult : MonoBehaviour
{
    public int id;
    public GameObject instantiateObj;
    public Action resultAction;
    public Action<GameObject> instantiateAction;
    public Vector3 instantiatePos;
    public void ShowResult()
    {
        resultAction?.Invoke();
        instantiateAction?.Invoke(instantiateObj);
    }
}
