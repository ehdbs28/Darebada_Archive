using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatInputModule : CommonModule
{
    public event Action<Vector3> OnMovementKeyPress = null;
    public event Action<float> OnRotateKeyPress = null;


    public override void UpdateModule()
    {
        UpdateMoveInput();
        UpdateRotateInput();
    }

    public override void FixedUpdateModule()
    {
    }

    private void UpdateMoveInput(){
        Vector3 moveInput = new Vector3(0, 0, Input.GetAxisRaw("Vertical"));
        OnMovementKeyPress?.Invoke(moveInput);
    }

    private void UpdateRotateInput(){
        OnRotateKeyPress?.Invoke(Input.GetAxisRaw("Horizontal"));
    }
}
