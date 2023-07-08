using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIFishingBuyElement : UIBuyElement
{
    private VisualElement _percentElement;
    private Label _levelLabel;
    private Label _valueLabel;

    FishingUpgradeTable _dataTable;

    int _idx = 0;

    public UIFishingBuyElement(VisualElement elementRoot, int idx) : base(elementRoot)
    {
        _idx = idx;
        _percentElement = elementRoot.Q<VisualElement>("value");
        _levelLabel = elementRoot.Q<Label>("level");
        _valueLabel = elementRoot.Q<Label>("value-text");

        _dataTable = GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.FishingUpgradeData) as FishingUpgradeTable;
    }

    protected override void AddEvent()
    {
        _buyButton.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<FishingUpgradeManager>().Upgrade(_idx);
            LabelUpdate();
            ChangeValue();
        });
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

        _levelLabel.text = $"Lv {level.ToString("D2")}";
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
}
