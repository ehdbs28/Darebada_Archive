using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour, IManager
{
    public static InventoryManager Instance;

    public List<VisualElement> FishList;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multy InventoryManager");
        }
        Instance = this;
    }

    public void SetUnitPos()
    {

    }

    public List<VisualElement> GetUnits()
    {
        return FishList;
    }

    public void InitManager()
    {

    }

    public void ResetManager()
    {

    }

    public void UpdateManager()
    {

    }
}
