using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBoatBuyElement : UIBuyElement
{
    private int _idx;

    private BoatBuyState _buyState;
    private Label _infoLabel;

    private List<UIBuyElement> _boatUIs;

    private BoatDataTable _dataTable;

    public UIBoatBuyElement(VisualElement elementRoot, int idx)
    {
        BoatData data = GameManager.Instance.GetManager<DataManager>().GetData(DataType.BoatData) as BoatData;

        _root = elementRoot;
        _idx = idx;
        _buyState = (BoatBuyState)data.BoatPurchaseDetail[_idx];
        _dataTable = GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.BoatData) as BoatDataTable;

        FindElement();
        AddEvent();

        ChangeState(_buyState);
    }

    protected override void AddEvent()
    {
        _buyBtn.RegisterCallback<ClickEvent>(e => {
            switch(_buyState){
                case BoatBuyState.Sale:
                {
                    GameManager.Instance.GetManager<MoneyManager>().Payment(_dataTable.DataTable[_idx].Price);
                    ChangeState(BoatBuyState.Bought);
                }
                    break;
                case BoatBuyState.Bought:
                    BoatChange();
                    break;
            }
        });
    }

    protected override void FindElement()
    {
        base.FindElement();
        _infoLabel = _root.Q<Label>("gold-text");
    }

    public void ListSet(List<UIBuyElement> list){
        _boatUIs = list;
    }

    private void BoatChange(){
        for(int i = 0; i < _boatUIs.Count; i++){
            if(i == _idx)
                continue;

            ChangeSelection(i, false);
        }
        ChangeSelection(_idx, true);
    }

    private void ChangeSelection(int idx, bool select){
        UIBoatBuyElement boatUI = (UIBoatBuyElement)_boatUIs[idx];
        
        if(select){
            boatUI.ChangeState(BoatBuyState.Equip);
            GameManager.Instance.GetManager<BoatManager>().SelectBoat(idx);
        }
        else{
            boatUI.ChangeState(BoatBuyState.Bought);
        }
    }

    public void ChangeState(BoatBuyState next){
        if(next == BoatBuyState.Sale){
            _root.RemoveFromClassList("unlock");
            _root.RemoveFromClassList("select");

            _infoLabel.text = $"$ {_dataTable.DataTable[_idx].Price}";
        }
        else if(next == BoatBuyState.Equip){
            _root.AddToClassList("unlock");
            _root.AddToClassList("select");
            _infoLabel.text = "장착중";
        }
        else if(next == BoatBuyState.Bought){
            _root.AddToClassList("unlock");
            _root.RemoveFromClassList("select");
            _infoLabel.text = "장착하기";
        }

        _buyState = next;

        BoatData data = GameManager.Instance.GetManager<DataManager>().GetData(DataType.BoatData) as BoatData;
        data.BoatPurchaseDetail[_idx] = (int)_buyState;
    }
}
