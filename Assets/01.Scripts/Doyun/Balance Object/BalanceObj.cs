using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceObj : MonoBehaviour
{
    [SerializeField] private BalanceUnit _heavyUnit;
    [SerializeField] private BalanceUnit _lightUnit;

    private void Update()
    {
        _heavyUnit.CheckAddWeight(true);
        _lightUnit.CheckAddWeight(false);

        if(_lightUnit.Weight > _heavyUnit.Weight)
        {
            _lightUnit.ValueDown();
            _heavyUnit.ValueUP();
        }
        else
        {
            _heavyUnit.ValueDown();
            _lightUnit.ValueUP();
        }
    }
}
