using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    public FishInfoSO fishSO;
    [SerializeField] TextMeshProUGUI _fishNameText;
    [SerializeField] TextMeshProUGUI _orderAndGenusText;
    [SerializeField] Image _icon;
    [SerializeField] TextMeshProUGUI _featuresText;
    [SerializeField] TextMeshProUGUI _biggestSizeText;
    [SerializeField] TextMeshProUGUI _biggestWeightText;
    [SerializeField] TextMeshProUGUI _habitatText;
    [SerializeField] TextMeshProUGUI _rarityText;
    [SerializeField] TextMeshProUGUI _necessityText;//필요한 장식 ID
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        if(fishSO.hadDonated)
        {
            _fishNameText.text = fishSO.fishName;
            _orderAndGenusText.text = fishSO.orderAndGenus;
            if (fishSO.hadDonated)
            {
                _icon.sprite = fishSO.icon;
            }
            else
            {
                _icon.sprite = fishSO.obscuredIcon;
            }
            _featuresText.text = fishSO.features;
            _biggestSizeText.text = fishSO.biggestSize + "mm";
            _biggestWeightText.text = fishSO.biggestWeight + "Kg";
            _habitatText.text = fishSO.habitat;
            string rarity;
            switch (fishSO.rarity)
            {
                case RARITY.S:
                    rarity = "S";
                    break;
                case RARITY.A:
                    rarity = "A";
                    break;
                case RARITY.B:
                    rarity = "B";
                    break;
                case RARITY.C:
                    rarity = "C";
                    break;
                case RARITY.D:
                    rarity = "D";
                    break;
                default:
                    rarity = "PARTY PA PARTY PARTY PARTY P A R T Y";
                    break;
            }
            _rarityText.text = rarity;
            _necessityText.text = fishSO.necessity;

        }else
        {
            _icon.sprite = fishSO.obscuredIcon;
            _fishNameText.text = "??";
            _orderAndGenusText.text = "??";

            _featuresText.text = "??";
            _biggestSizeText.text = "??";
            _biggestWeightText.text = "??";
            _habitatText.text = "??";
            _rarityText.text = "??";
            _necessityText.text = "??";
        }
    }
}
