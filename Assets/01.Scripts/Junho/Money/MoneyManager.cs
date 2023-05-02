using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    public long money;

    [SerializeField] private TextMeshProUGUI goldTxt;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multy MoneyManager");
        }
        Instance = this;
    }

    private void Start()
    { 

    }

    private void Update()
    {
        if (money < 0) money = 0;
        goldTxt.text = $"Gold : {money}";
    }

    public void ItemSell(int cost)
    {
        if (cost < 0) return;

        money += cost;

        /*
         ������ ���ݸ�ŭ �� �߰���Ű��
         */
    }

    public void ItemUpgrade(ref int upgrade, int cost)
    {
        if (cost > money) return;

        upgrade++;    
        money -= cost;

        /*
         ���׷��̵� ������ ������ ���ų� ���׷��̵� ��ġ�� �̹� �ִ�� ���
         ���׷��̵� ��ġ �ø���
         ���ݸ�ŭ ������ ����
         */
    }
}
