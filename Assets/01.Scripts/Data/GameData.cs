using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : SaveData
{
    public float LastWorldTime = 0.0f;
    public int LastTotalDay = 1;

    public int LastYear = 0;
    public int LastMonth = 3;
    public int LastDay = 1;

    public override void Reset()
    {
    }
}
