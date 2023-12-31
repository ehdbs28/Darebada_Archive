using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FishDexManager : MonoBehaviour
{
    [SerializeField] InfoPanel infoPanel;
    [SerializeField] Transform iconParent;
    public GameObject fishIconObject;
    [SerializeField] List<FishInfoSO> infoSOList;
    public Dictionary<string, FishInfoSO> fishInfos = new Dictionary<string, FishInfoSO>();
    private void Awake()
    {
        Init();
    }
    public void Init()
    {

        foreach (FishInfoSO so in infoSOList)
        {
            fishInfos.Add(so.fishName, so);
        }
        List<string> keys = fishInfos.Keys.ToList<string>();
        for (int i = 0; i < keys.Count; i++)
        {
            GameObject obj = Instantiate(fishIconObject, iconParent);
            FishIcon icon = obj.GetComponent<FishIcon>();
            icon.fishInfo = fishInfos[keys[i]];
            icon.Show();
        }
    }
    public void OpenDex(FishInfoSO fishSo)
    {
        infoPanel.fishSO= fishSo;
        infoPanel.gameObject.SetActive(true);
    }
}
