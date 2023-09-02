using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "SO/VisualSO/FishVisual")]
public class FishViual : ScriptableObject{
    public Mesh VisualMesh;
    public Sprite Profile;
    public Material MainMat;
}