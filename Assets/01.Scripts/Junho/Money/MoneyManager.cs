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

    public void ItemSell(object item)
    {
        // if (item == null) return;

        //money += item.cost;

        /*
         ������ ���ݸ�ŭ �� �߰���Ű��
         */
    }

    public void ItemBuy(object item, object buy)
    {
        //if (item == null) return;
        //if (item.cost > money) return;
        
        //money -= item.cost;
        //buy++;
        
        /*
         ������ ������ ��������
         ������ ���� (�����̶� �����ϰ� �ø�)
         */

    }

    public void ItemUpgrade(object item)
    {
        // if (item == null) return;
        // if (item.cost > money || item.maxUpgrade <= item.upgrade) return;

        // item.upgrade++;    
        // money -= item.cost;

        /*
         ���׷��̵� ������ ������ ���ų� ���׷��̵� ��ġ�� �̹� �ִ�� ���
         ���׷��̵� ��ġ �ø���
         ���ݸ�ŭ ������ ����
         */
    }
}
