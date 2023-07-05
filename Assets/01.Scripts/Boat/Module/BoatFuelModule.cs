using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoatFuelModule : CommonModule<BoatController>
{
    private float _maxFuel{
        get{
            return _controller.DataSO.MaxFuel;
        }
    }

    private float _currentFuel{
        get{
            return _controller.BoatActionData.CurrentFuel;
        }
        set{
            _controller.BoatActionData.CurrentFuel = value;
        }
    }

    public UnityEvent OnReduceEvent;
    public UnityEvent OnDestroyBoatEvent;

    public override void SetUp(Transform rootTrm)
    {
        base.SetUp(rootTrm);

        _currentFuel = _maxFuel;
    }

    public override void FixedUpdateModule()
    {
    }

    public override void UpdateModule()
    {
    }

    public void SetFuel(float value){
        OnReduceEvent?.Invoke();
        _currentFuel = Mathf.Clamp(_currentFuel + value, 0, _maxFuel);

        if(_currentFuel <= 0){
            OnDestroyBoatEvent?.Invoke();
        }
    }
}
