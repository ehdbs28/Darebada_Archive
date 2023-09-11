using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/DecoVisual")]
public class DecoVisualSO : ScriptableObject
{
    public string Name;
    public Mesh mesh;
    public Material mainMat;
    public Sprite sprite;
}