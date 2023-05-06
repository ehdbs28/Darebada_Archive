using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommonModule<T> : MonoBehaviour, Module where T : ModuleController
{
    protected T _controller;

    public virtual void SetUp(Transform rootTrm)
    {
        _controller = rootTrm.GetComponent<T>();
    }

    public abstract void UpdateModule();
    public abstract void FixedUpdateModule();
}
