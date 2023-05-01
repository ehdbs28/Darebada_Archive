using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDexManager : MonoBehaviour
{
    [SerializeField] InfoPanel infoPanel;
    public void OpenDex(FishInfoSO fishSo)
    {
        infoPanel.fishSO= fishSo;
        infoPanel.gameObject.SetActive(true);
    }
}
