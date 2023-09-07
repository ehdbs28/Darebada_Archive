using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public sealed class UIFishingBuyElement : UIInteractionElement
{
    private VisualElement _visualImage;
    
    private VisualElement _percentElement;
    
    private Label _levelLabel;
    private Label _valueLabel;
    private Label _priceLabel;

    FishingUpgradeTable _dataTable;

    int _idx = 0;

    public UIFishingBuyElement(VisualElement elementRoot, int idx) : base(elementRoot)
    {
        _idx = idx;
        _dataTable = GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.FishingUpgradeData) as FishingUpgradeTable;
        FindElement();
        AddEvent();
        
        LabelUpdate();
        ChangeValue();
        ChangePrice();
    }

    protected override void AddEvent()
    {
        _interactionBtn.RegisterCallback<ClickEvent>(e => {
            if (GameManager.Instance.GetManager<FishingUpgradeManager>().Upgrade(_idx))
            {
                LabelUpdate();
                ChangeValue();
                ChangePrice();
                PlayParticle();
            }
        });
    }

    protected override void FindElement()
    {
        base.FindElement();
        _visualImage = _root.Q("fishing-image");
        _percentElement = _root.Q<VisualElement>("value");
        _levelLabel = _root.Q<Label>("level");
        _valueLabel = _root.Q<Label>("value-text");
        _priceLabel = _root.Q<Label>("gold-text");
    }

    private void PlayParticle()
    {
        Vector2 particlePos = GameManager.Instance.GetManager<UIManager>().GetElementPos(_visualImage, new Vector2(0.5f, 0.5f));
        
        PoolableUIParticle particle = GameManager.Instance.GetManager<PoolManager>().Pop("UpgradeFeedback") as PoolableUIParticle;
        particle.SetPoint(particlePos);
        particle.Play();
    }

    private void LabelUpdate(){
        FishingData data = GameManager.Instance.GetManager<DataManager>().GetData(DataType.FishingData) as FishingData;

        int level = 0;

        switch(_idx){
            case 0:
                level = data.StringLength_Level;
            break;
            case 1:
                level = data.StringStrength_Level;
            break;
            case 2:
                level = data.ThrowPower_Level;
            break;
        }

        _levelLabel.text = $"Lv {level:D2}";
        _valueLabel.text = $"{_dataTable.DataTable[_idx].Value[level - 1]}";
    }

    private void ChangeValue(){
        FishingData data = GameManager.Instance.GetManager<DataManager>().GetData(DataType.FishingData) as FishingData;
        
        int cur = 0;
        int max = _dataTable.DataTable[_idx].MaxLevel;
        float percent; 
        
        switch(_idx){
            case 0:
                cur = data.StringLength_Level;
            break;
            case 1:
                cur = data.StringStrength_Level;
            break;
            case 2:
                cur = data.ThrowPower_Level;
            break;
        }

        percent = (float)cur / (float)max;
        _percentElement.transform.scale = new Vector3(percent, 1);
    }

    private void ChangePrice()
    {
        FishingData data = GameManager.Instance.GetManager<DataManager>().GetData(DataType.FishingData) as FishingData;

        int level = 0;

        switch(_idx){
            case 0:
                level = data.StringLength_Level;
                break;
            case 1:
                level = data.StringStrength_Level;
                break;
            case 2:
                level = data.ThrowPower_Level;
                break;
        }

        _priceLabel.text = $"{_dataTable.DataTable[_idx].Price[level - 1]}";
    }
}
