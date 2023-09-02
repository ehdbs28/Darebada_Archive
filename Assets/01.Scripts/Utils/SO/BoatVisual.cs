using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "SO/VisualSO/BoatVisual")]
public class BoatViual : ScriptableObject{
    public Mesh VisualMesh;
    public Material MainMat;
}