using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : SaveData
{
    public float LastWorldTime = 0.0f;
    public int LastDay = 0;

    public override void Reset()
    {
    }
}
