using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommonModule : MonoBehaviour, BoatModule
{
    protected BoatController _controller;

    public virtual void SetUp(Transform rootTrm)
    {
        _controller = rootTrm.GetComponent<BoatController>();
    }

    public abstract void UpdateModule();
    public abstract void FixedUpdateModule();
}
