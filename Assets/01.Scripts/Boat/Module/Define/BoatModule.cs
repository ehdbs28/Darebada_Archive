using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BoatModule
{
    public void SetUp(Transform rootTrm);
    public void UpdateModule();
    public void FixedUpdateModule();
}
