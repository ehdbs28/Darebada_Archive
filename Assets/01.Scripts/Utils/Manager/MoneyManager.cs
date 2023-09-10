using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoneyManager : IManager
{
    private GameData _gameData
    {
        get => (GameData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.GameData); 
    }

    public Action<int> OnGoldChange = null; 
    
    public void InitManager()
    {
    }

    public void UpdateManager()
    {
    }

    public void AddMoney(int value)
    {
        _gameData.HoldingGold += value;
        OnGoldChange?.Invoke(_gameData.HoldingGold);
    }

    public void Payment(int value, Action CallBack)
    {
        _gameData.HoldingGold -= value;
        CallBack?.Invoke();
        OnGoldChange?.Invoke(_gameData.HoldingGold);
        if (_gameData.HoldingGold >= value)
        {
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
        }
    }

    public void ResetManager(){}
}
