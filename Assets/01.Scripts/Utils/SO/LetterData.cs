using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/LetterData")]
public class LetterData : ScriptableObject
{
    public LetterType Type;

    public string Title;
    [TextArea]
    public string Desc;
    public string Date;
    public string From;
}
