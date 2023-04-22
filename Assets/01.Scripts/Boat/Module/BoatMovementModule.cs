using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovementModule : CommonModule
{
    private Vector3 _movement;
    private Vector3 _dir;

    public override void SetUp(Transform rootTrm)
    {
        base.SetUp(rootTrm);


    }

    public override void UpdateModule()
    {
        // for Test
        // 나중에 모바일 환경으로 교체 해야함
        _dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0); 
    }
}
