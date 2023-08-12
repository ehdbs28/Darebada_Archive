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
    
    public bool CompleteSetting { get; set; }
    
    protected override void Awake()
    {
        base.Awake();
        _actionData = GetComponent<FishActionData>();
    }

    protected override void Update()
    {
        if (!CompleteSetting)
            return;
        
        base.Update();
    }

    protected override void FixedUpdate()
    {
        if (!CompleteSetting)
            return;
        
        base.FixedUpdate();
    }

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }

    public void SetUp(FishDataUnit data, BoxCollider bound)
    {
        _dataUnit = data;
        // Should Check Visual and Size Change
        GetModule<FishMovementModule>().SetBound(bound);
        CompleteSetting = true;
    }
}
