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

    public int speed;
    public int storage;
    public int durability;
    public int power;
    public int length;
    public int strength;

    private void Awake()
    {
        _calculater = GetComponent<FishingUpgradeCalculater>();
    }
    private void FixedUpdate()
    {
        _speedCostText.text = _calculater.CalcShipSpeedCost(speed).ToString();
        _storageCostText.text =_calculater.CalcShipStorageCost(storage).ToString();
        _durabilityCostText.text =_calculater.CalcShipStorageCost(durability).ToString();
        _powerCostText.text =_calculater.CalcFishingStickWeight(power).ToString();
        _lengthCostText.text =_calculater.CalcFishingStickLength(length).ToString();
        _strengthCostText.text =_calculater.CalcFishingStickStrength(strength).ToString();
    }
    public void OnUpgradeButton(UPGRADETYPE upgradeType)
    {
        //switch (upgradeType)
        //{
        //    case UPGRADETYPE.SPEED:
        //        MoneyManager.Instance.ItemUpgrade(ref speed, _calculater.CalcShipSpeedCost(speed));
        //        break;
        //    case UPGRADETYPE.STORAGE:
        //        MoneyManager.Instance.ItemUpgrade(ref storage, _calculater.CalcShipStorageCost(storage));
        //        break;
        //    case UPGRADETYPE.DURABILITY:
        //        MoneyManager.Instance.ItemUpgrade(ref durability, _calculater.CalcShipDurability(durability));
        //        break;
        //    case UPGRADETYPE.POWER:
        //        MoneyManager.Instance.ItemUpgrade(ref power, _calculater.CalcFishingStickWeight(power));
        //        break;
        //    case UPGRADETYPE.LENGTH:
        //        MoneyManager.Instance.ItemUpgrade(ref length, _calculater.CalcFishingStickLength(length));
        //        break;
        //    case UPGRADETYPE.STRENGTH:
        //        MoneyManager.Instance.ItemUpgrade(ref strength, _calculater.CalcFishingStickStrength(strength));
        //        break;
        //}
    }
}
