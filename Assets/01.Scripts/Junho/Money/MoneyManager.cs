using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    public long money;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multy MoneyManager");
        }
        Instance = this;
    }

    private void Update()
    {
        if (money < 0) money = 0;
    }

    public void ItemSell()
    {
        /*
         �Ű������� �� ������ ��������
         �Ű������� ������ �������� ������ ���� ���ϱ�
         */
    }

    public void ItemUpgrade()
    {
        /*
         �Ű������� ���׷��̵� ��������
         ���׷��̵� ��뺸�� ���� ������ ���׷��̵�
         �ƴϸ� ���
         */
    }
}
