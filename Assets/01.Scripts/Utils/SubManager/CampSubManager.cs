using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampSubManager : MonoBehaviour, IManager
{
    [SerializeField]
    private MeshFilter _boatMeshFilter;
    
    [SerializeField]
    private MeshRenderer _boatMeshRenderer;

    public void EnterSceneEvent()
    {
        GameManager.Instance.Managers.Add(this);
        BoatDataUnit boatDataUnit = GameManager.Instance.GetManager<BoatManager>().CurrentBoatData;
        SetBoatVisual(boatDataUnit);
    }

    public void ExitSceneEvent()
    {
        GameManager.Instance.Managers.Remove(this);
    }

    public void SetBoatVisual(BoatDataUnit dataUnit)
    {
        _boatMeshFilter.mesh = dataUnit.Visual.VisualMesh;
        _boatMeshRenderer.material = dataUnit.Visual.MainMat;
    }

    public void ResetManager()
    {
    }

    public void InitManager()
    {
    }

    public void UpdateManager()
    {
    }
}
