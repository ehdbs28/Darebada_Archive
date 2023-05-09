using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeCostManager : MonoBehaviour
{
    public enum UPGRADETYPE
    {
        SPEED,
        STORAGE,
        DURABILITY,
        POWER,
        LENGTH,
        STRENGTH
    }
    FishingUpgradeCalculater _calculater;
    [SerializeField] TextMeshProUGUI _speedCostText;
    [SerializeField] TextMeshProUGUI _storageCostText;
    [SerializeField] TextMeshProUGUI _durabilityCostText;
    [SerializeField] TextMeshProUGUI _powerCostText;
    [SerializeField] TextMeshProUGUI _lengthCostText;
    [SerializeField] TextMeshProUGUI _strengthCostText;

    public float speed;
    public float storage;
    public float durability;
    public float power;
    public float length;
    public float strength;

    private void Awake()
    {
        _calculater = GetComponent<FishingUpgradeCalculater>();
    }
    private void FixedUpdate()
    {
        _speedCostText.text = _calculater.CalcShipSpeedCost((int)speed).ToString();
        _storageCostText.text =_calculater.CalcShipStorageCost((int)storage).ToString();
        _durabilityCostText.text =_calculater.CalcShipStorageCost((int)durability).ToString();
        _powerCostText.text =_calculater.CalcFishingStickWeight((int)power).ToString();
        _lengthCostText.text =_calculater.CalcFishingStickLength((int)length).ToString();
        _strengthCostText.text =_calculater.CalcFishingStickStrength((int)strength).ToString();
    }
    public void OnUpgradeButton(UPGRADETYPE upgradeType)
    {
        switch (upgradeType)
        {
            case UPGRADETYPE.SPEED:
                MoneyManager.Instance.ItemUpgrade(ref speed, _calculater.CalcShipSpeedCost((int)speed),1);
                break;
            case UPGRADETYPE.STORAGE:
                MoneyManager.Instance.ItemUpgrade(ref storage, _calculater.CalcShipStorageCost((int)storage),1);
                break;
            case UPGRADETYPE.DURABILITY:
                MoneyManager.Instance.ItemUpgrade(ref durability, _calculater.CalcShipDurability((int)durability),1);
                break;
            case UPGRADETYPE.POWER:
                MoneyManager.Instance.ItemUpgrade(ref power, _calculater.CalcFishingStickWeight((int)power),1);
                break;
            case UPGRADETYPE.LENGTH:
                MoneyManager.Instance.ItemUpgrade(ref length, _calculater.CalcFishingStickLength((int)length),1);
                break;
            case UPGRADETYPE.STRENGTH:
                MoneyManager.Instance.ItemUpgrade(ref strength, _calculater.CalcFishingStickStrength((int)strength), 1);
                break;
        }
    }
}
