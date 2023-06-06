using UnityEngine;
using UnityEngine.UI;

public class UpgradeFixUI : MonoBehaviour
{
    [SerializeField] GameObject fixUIpanel;
    [SerializeField] GameObject upgradeDefaultUI;
    [SerializeField] GameObject fixDefaultUI;

    [Header("Ship Upgrade Button")]
    [SerializeField] Button shipSpeedUpgradeBtn;
    [SerializeField] Button shipInventoryUpgradeBtn;
    [SerializeField] Button shipDurablityUpgradeBtn;
    [Header("Ship fix Button")]
    [SerializeField] Button shipFixBtn;
    [Header("FishingRod Upgrade Button")]
    [SerializeField] Button fishingRodPowerUpgradeBtn;
    [SerializeField] Button fishingRodLengthUpgradeBtn;
    [Header("FishingRod fix Button")]
    [SerializeField] Button fishingRodFixBtn;
    
    GameObject alreadyOpenUI;

    BoatData boatData;
    FishingData fishingData;
    [SerializeField] InventoryGrid inventoryGrid;

    bool isStateUpgrade = true;

    private void Start()
    {
        // shipSpeedUpgradeBtn.onClick.AddListener(() => MoneyManager.Instance.ItemUpgrade(ref boatData.BoatMaxSpeed, 5 ,1000));
        // shipInventoryUpgradeBtn.onClick.AddListener(() =>
        // {
        //     MoneyManager.Instance.ItemUpgrade(ref inventoryGrid.gridSizeHeight, 1, 1000);
        //     //inventoryGrid.Init(inventoryGrid.gridSizeWidth, inventoryGrid.gridSizeHeight);
        // });
        // shipDurablityUpgradeBtn.onClick.AddListener(() => MoneyManager.Instance.ItemUpgrade(ref boatData.MaxDurablity, 5 ,1000));

        // fishingRodPowerUpgradeBtn.onClick.AddListener(() => MoneyManager.Instance.ItemUpgrade(ref fishingData.MaxChargingPower, 5, 1000));
        // fishingRodLengthUpgradeBtn.onClick.AddListener(() => MoneyManager.Instance.ItemUpgrade(ref fishingData.fishingRodLength, 5, 1000));

        // shipFixBtn.onClick.AddListener(() => {
        //     if (boatData.CurrentDurablity == 0)
        //     {
        //         shipFixBtn.enabled = false;
        //     }
        //     boatData.CurrentDurablity = boatData.MaxDurablity;
        // });

        // alreadyOpenUI = upgradeDefaultUI;
    }

    public void OnUpgradeUI() {
        alreadyOpenUI?.SetActive(false);
        
        isStateUpgrade = true;
        upgradeDefaultUI.SetActive(true);

        alreadyOpenUI = upgradeDefaultUI;
    }
    public void OnFixUI()
    {
        alreadyOpenUI?.SetActive(false);

        isStateUpgrade = false;
        fixDefaultUI.SetActive(true);

        alreadyOpenUI = fixDefaultUI;
    }

    public void OnUpgradeOpen(GameObject openUI)
    {
        if (isStateUpgrade)
        {
            alreadyOpenUI?.gameObject.SetActive(false);

            openUI.SetActive(true);
            alreadyOpenUI = openUI;
        }
    }
    public void OnFixOpen(GameObject openUI)
    {
        if (!isStateUpgrade)
        {
            alreadyOpenUI?.gameObject.SetActive(false);

            openUI.SetActive(true);
            alreadyOpenUI = openUI;
        }
    }


}
