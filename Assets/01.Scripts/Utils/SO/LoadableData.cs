using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LoadableData : ScriptableObject
{
    public abstract void SetUp(string[] dataArr);
}
