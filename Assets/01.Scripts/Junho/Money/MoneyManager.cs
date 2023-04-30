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
         매개변수로 팔 아이템 가져오기
         매개변수로 가져온 아이템의 가격을 돈에 더하기
         */
    }

    public void ItemUpgrade()
    {
        /*
         매개변수로 업그레이드 가져오기
         업그레이드 비용보다 돈이 많으면 업그레이드
         아니면 취소
         */
    }
}
