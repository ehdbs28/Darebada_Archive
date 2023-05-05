using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Net.Sockets;

public class UIManager : MonoBehaviour
{
    [SerializeField] public StatePanel fishStatePanel;
    [SerializeField] public StatePanel shopStatePanel;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _cleanScoreText;
    [SerializeField] private TextMeshProUGUI _artScoreText;
    [SerializeField] private TextMeshProUGUI _reputationText;
    [SerializeField] private TextMeshProUGUI _fishCostText;
    [SerializeField] private TextMeshProUGUI _shopCostText;
    [SerializeField] private TextMeshProUGUI _shopAmountText;
    [SerializeField] private GameObject AddPanel;
    private void FixedUpdate()
    {
        _goldText.text = AquariumManager.Instance.Gold.ToString();
        _cleanScoreText.text = AquariumManager.Instance.CleanScore.ToString();
        _artScoreText.text = AquariumManager.Instance.ArtScore.ToString();
        _reputationText.text = AquariumManager.Instance.Reputation.ToString();
        if (fishStatePanel.upgradeObj)
        {
            _fishCostText.text = fishStatePanel.upgradeObj.GetComponent<Fishbowl>().Cost.ToString();
        }
        if(shopStatePanel.upgradeObj)
        {
            _shopCostText.text = shopStatePanel.upgradeObj.GetComponent<SnackShop>().cost.ToString();
            _shopAmountText.text = shopStatePanel.upgradeObj.GetComponent<SnackShop>().amount.ToString();
        }

    }
    public void OnOffPanel()
    {
        AddPanel.SetActive(!AddPanel.activeSelf);
    }
}
