using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoatDurabilityModule : CommonModule<BoatController>
{
    private float _maxDurability{
        get{
            BoatData data = GameManager.Instance.GetManager<DataManager>().GetData(DataType.BoatData) as BoatData;
            return data.MaxDurablity;
        }
    }
    private float _currentDurability{
        get{
            return _controller.BoatActionData.CurrentDurability;
        }
        set{
            _controller.BoatActionData.CurrentDurability = value;
        }
    }

    public UnityEvent OnReduceEvent;
    public UnityEvent OnDestroyBoatEvent;

    public override void SetUp(Transform rootTrm)
    {
        base.SetUp(rootTrm);

        _currentDurability = _maxDurability;
    }

    public override void FixedUpdateModule()
    {
    }

    public override void UpdateModule()
    {
    }

    public void SetDurability(float damage){
        OnReduceEvent?.Invoke();
        _currentDurability = Mathf.Clamp(_currentDurability + damage, 0, _maxDurability);

        if(_currentDurability <= 0){
            OnDestroyBoatEvent?.Invoke();
        }
    }
}
