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
         아이템 가격만큼 돈 추가시키기
         */
    }

    public void ItemUpgrade(ref int upgrade, int cost)
    {
        if (cost > money) return;

        upgrade++;    
        money -= cost;

        /*
         업그레이드 가격이 돈보다 높거나 업그레이드 수치가 이미 최대면 취소
         업그레이드 수치 올리기
         가격만큼 돈에서 빼기
         */
    }
}
