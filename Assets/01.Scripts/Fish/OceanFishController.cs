using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class OceanFishController : ModuleController
{
    private FishDataUnit _dataUnit;
    public FishDataUnit DataUnit => _dataUnit;

    private FishActionData _actionData;
    public FishActionData ActionData => _actionData;

    protected override void Awake()
    {
        base.Awake();
        _actionData = GetComponent<FishActionData>();
    }

    public void SetUp(FishDataUnit data)
    {
        _dataUnit = data;
    }
}
