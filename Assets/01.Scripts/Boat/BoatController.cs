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
    public BoatDataUnit CurBoatData => _curBoatData;

    public BoatData BoatData => GameManager.Instance.GetManager<DataManager>().GetData(DataType.BoatData) as BoatData;

    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    
    protected override void Awake() {
        base.Awake();
        _boatActionData = GetComponent<BoatActionData>();
        _meshFilter = transform.Find("Visual").GetComponent<MeshFilter>();
        _meshRenderer = _meshFilter.GetComponent<MeshRenderer>();
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

        _meshFilter.mesh = data.Visual.VisualMesh;
        _meshRenderer.material = data.Visual.MainMat;
    }
}
