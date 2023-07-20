using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIShopUnit : UIBuyElement
{
    private VisualTreeAsset _templete;
    private ShopItemUnit _unit;

    private ScrollView _parent;

    private VisualElement _imageBox;
    private Label _nameLabel;
    private Label _descLabel;
    private Label _priceLabel;

    private bool _isEquip;

    private List<UIBuyElement> _units;

    public UIShopUnit(ScrollView parent, VisualTreeAsset templete, ShopItemUnit unit, int idx){
        _templete = templete;
        _parent = parent;
        _isEquip = false;
        _unit = unit;
    }

    public void Generate(){
        _root = _templete.Instantiate();
        _root = _root.Q<VisualElement>("shop-item");
        _parent.Add(_root);

        FindElement();
        Setting();
        AddEvent();
    }

    private void Setting(){
        _imageBox.style.backgroundImage = new StyleBackground(_unit.Image);
        _nameLabel.text = _unit.Name;
        _descLabel.text = _unit.Desc;
        _priceLabel.text = $"{_unit.Price} $";
    }

    protected override void FindElement(){
        base.FindElement();
        _imageBox = _root.Q<VisualElement>("item-image");
        _nameLabel = _root.Q<Label>("item-name");
        _descLabel = _root.Q<Label>("item-desc");
        _priceLabel = _root.Q<Label>("gold-text");
    }

    protected override void AddEvent(){
        _buyBtn.RegisterCallback<ClickEvent>(e => {
            if(_isEquip)
                return;

            GameManager.Instance.GetManager<MoneyManager>().Payment(_unit.Price);
            // fishing -> item select
            for(int i = 0; i < _units.Count; i++){
                ((UIShopUnit)_units[i]).Toggle(false);
            }
            Toggle(true);
        });
    }

    public void Toggle(bool value){
        _isEquip = value;
        if(value){
            _root.AddToClassList("equip");
        }
        else{
            _root.RemoveFromClassList("equip");
        }
    }

    public void ListSet(List<UIBuyElement> list){
        _units = list;
    }
}
