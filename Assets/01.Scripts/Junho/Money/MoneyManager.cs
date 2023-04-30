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
         아이템 가격만큼 돈 추가시키기
         */
    }

    public void ItemBuy(object item, object buy)
    {
        //if (item == null) return;
        //if (item.cost > money) return;
        
        //money -= item.cost;
        //buy++;
        
        /*
         아이템 가격을 가져오고
         무엇을 살지 (스텟이라 가정하고 올림)
         */

    }

    public void ItemUpgrade(object item)
    {
        // if (item == null) return;
        // if (item.cost > money || item.maxUpgrade <= item.upgrade) return;

        // item.upgrade++;    
        // money -= item.cost;

        /*
         업그레이드 가격이 돈보다 높거나 업그레이드 수치가 이미 최대면 취소
         업그레이드 수치 올리기
         가격만큼 돈에서 빼기
         */
    }
}
