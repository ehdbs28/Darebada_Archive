using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowingVCam : VCam
{
    [SerializeField]
    private Transform _target;

    public override void UpdateCam()
    {
    }
}
