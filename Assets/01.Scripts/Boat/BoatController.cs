using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : ModuleController
{
    private BoatActionData _boatActionData;
    public BoatActionData BoatActionData => _boatActionData;
    
    [SerializeField]
    private BoatDataUnit _curBoatData;
    public BoatDataUnit CurBoatData
    {
        get => _curBoatData;
        set => _curBoatData = value;
    }

    public BoatData BoatData => GameManager.Instance.GetManager<DataManager>().GetData(DataType.BoatData) as BoatData;

    protected override void Awake() {
        base.Awake();
        _boatActionData = GetComponent<BoatActionData>();
    }

    protected override void Update()
    {
        if(_boatActionData.IsDestroy == true)
            return;

        base.Update();
        _boatActionData.Forward = transform.forward;
    }

    public void SetBoat(BoatDataUnit data){
        _curBoatData = data;

        // 추가 작업
    }
}
