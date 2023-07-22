using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIShopUnit : UIBuyElement
{
    private readonly VisualTreeAsset _templete;
    private readonly ShopItemUnit _unit;

    private readonly ScrollView _parent;

    private VisualElement _imageBox;
    private Label _nameLabel;
    private Label _descLabel;
    private Label _priceLabel;

    public UIShopUnit(ScrollView parent, VisualTreeAsset templete, ShopItemUnit unit){
        _templete = templete;
        _parent = parent;
        _unit = unit;
    }

    public void Generate(){
        _root = _templete.Instantiate();
        _root = _root.Q<VisualElement>("shop-item");
        _parent.Add(_root);
        
        Toggle(!GameManager.Instance.GetManager<ShopManager>().IsInStock(_unit.Index));

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
            if(!GameManager.Instance.GetManager<ShopManager>().IsInStock(_unit.Index))
                return;

            GameManager.Instance.GetManager<ShopManager>().PurchaseItem(_unit.Index);            
            if(!GameManager.Instance.GetManager<ShopManager>().IsInStock(_unit.Index))
                Toggle(true);
        });
    }

    private void Toggle(bool value){
        if(value){
            _root.AddToClassList("soldout");
        }
        else{
            _root.RemoveFromClassList("soldout");
        }
    }
}
