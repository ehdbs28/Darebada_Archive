using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatInputModule : CommonModule
{
    public event Action<float> OnMovementKeyPress = null;
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
        OnMovementKeyPress?.Invoke(Input.GetAxisRaw("Vertical"));
    }

    private void UpdateRotateInput(){
        OnRotateKeyPress?.Invoke(Input.GetAxisRaw("Horizontal"));
    }
}
