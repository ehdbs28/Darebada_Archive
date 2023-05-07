using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    static public AchievementManager Instance;
    public List<AchievementSO> achievementSOs= new List<AchievementSO>();//�߰��� ����������
    public Dictionary<string, Achievement> achievementWithNames = new Dictionary<string, Achievement>();//������������ �����ϱ� ���� ��ųʸ�

    [SerializeField] private InfoPanel _infoPanel;
    [SerializeField] private GameObject _achievementObject;
    [SerializeField] private Transform _parentTrm;
    private void Awake()
    {
        _infoPanel = FindObjectOfType<InfoPanel>();

        Init();
    }

    public void OpenDex(FishInfoSO fishSo)
    {
        _infoPanel.fishSO = fishSo;
        _infoPanel.gameObject.SetActive(true);
    }
    private void Init()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
        foreach(AchievementSO so in achievementSOs)
        {
            Debug.Log("ASdf");
            GameObject temp= Instantiate(_achievementObject, _parentTrm);
            temp.name = so.name;
            Achievement ac = temp.GetComponent<Achievement>();
            ac.arcSO = so;
            ac.Init();
            
            achievementWithNames.Add(so.achievenemtName, ac);
        }
    }
}
