using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaners : MonoBehaviour
{
    public static Cleaners Instance;
    public int cleanersAmount;
    public int cleanersEffect;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }
    public void AddCleaners()
    {
        if(AquariumManager.Instance.Gold >= cleanersAmount * cleanersAmount * 100)
        {
            AquariumManager.Instance.Gold -= cleanersAmount * cleanersAmount * 100;
            cleanersAmount++;
        }
    }
}
