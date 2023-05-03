using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LetterUnit : MonoBehaviour
{
    private LetterType _type;
    private string _title;
    private string _desc;
    private string _date;
    private string _from;

    

    public LetterUnit(LetterData data){
        _type = data.Type;
        _title = data.Title;
        _desc = data.Desc;
        _date = data.Date;
        _from = data.From;
    }

    
}
