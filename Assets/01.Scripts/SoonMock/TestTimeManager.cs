using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class TestTimeManager : MonoBehaviour
{
    static public float time = 0;
    public float timeSpeed;
    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime * timeSpeed;
        if(time -240 >= 0)
        {
            OnNewDay();
            time = 0;
        }
    }
    private void OnNewDay()
    {
        float downCount = Mathf.Clamp(AquariumManager.Instance.Reputation * AquariumManager.Instance.aquaObject.Count * 0.01f, 0, 35);
        Debug.Log(AquariumManager.Instance.Reputation*AquariumManager.Instance.aquaObject.Count * 0.01f);
        AquariumManager.Instance.CleanScore -= (int)Mathf.Round(downCount);
        AquariumManager.Instance.CleanScore += Cleaners.Instance.cleanersAmount * Cleaners.Instance.cleanersEffect;
        AquariumManager.Instance.CleanScore %= 100;
        //임시 수익
        AquariumManager.Instance.Gold += (int)(AquariumManager.Instance.Reputation * AquariumManager.Instance.aquaObject.Count * AquariumManager.Instance.EntranceFee);
    }
}
