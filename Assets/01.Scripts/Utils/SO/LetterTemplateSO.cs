using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/LetterTemplete")]
public class LetterTemplateSO : ScriptableObject
{
    public List<string> titles;
    public List<string> froms;
    public List<string> descs;
}
