using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : ModuleController
{
    private BoatActionData _boatActionData;
    public BoatActionData BoatActionData => _boatActionData;
    public BoatData BoatData => GameManager.Instance.GetManager<DataManager>().GetData(Core.DataType.BoatData) as BoatData;

    protected override void Awake() {
        base.Awake();

        _boatActionData = GetComponent<BoatActionData>();
    }
}
