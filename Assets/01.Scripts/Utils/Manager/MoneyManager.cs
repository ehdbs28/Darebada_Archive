using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoneyManager : IManager
{
    public void InitManager()
    {
    }

    public void UpdateManager()
    {
    }

    public void Payment(float value){
        Debug.Log($"지불 금액 { value }");
    }

    public void ResetManager(){}
}
