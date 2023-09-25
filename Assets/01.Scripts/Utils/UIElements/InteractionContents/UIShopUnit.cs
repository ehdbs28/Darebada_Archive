using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public sealed class UIShopUnit : UIInteractionElement
{
    private readonly ShopItemUnit _unit;

    private VisualElement _imageBox;
    private Label _nameLabel;
    private Label _descLabel;
    private Label _priceLabel;

    private VisualElement _goldElem;

    public UIShopUnit(VisualElement root, VisualElement goldElem, ShopItemUnit unit) : base(root)
    {
        _unit = unit;
        _goldElem = goldElem;
        
        Toggle(!GameManager.Instance.GetManager<ShopManager>().IsInStock(_unit.Index));

        FindElement();
        Setting();
        AddEvent();
    }

    private void Setting(){
        _imageBox.style.backgroundImage = new StyleBackground(_unit.Image);
        _nameLabel.text = _unit.Name;
        _descLabel.text = _unit.Desc;
        _priceLabel.text = $"{_unit.Price}";
    }

    protected override void FindElement(){
        base.FindElement();
        _imageBox = _root.Q<VisualElement>("item-image");
        _nameLabel = _root.Q<Label>("item-name");
        _descLabel = _root.Q<Label>("item-desc");
        _priceLabel = _root.Q<Label>("gold-text");
    }

    protected override void AddEvent(){
        _interactionBtn.RegisterCallback<ClickEvent>(e => {
            if(!GameManager.Instance.GetManager<ShopManager>().IsInStock(_unit.Index))
                return;

            if(GameManager.Instance.GetManager<ShopManager>().PurchaseItem(_unit.Index))            
                PlayParticle();
            
            if(!GameManager.Instance.GetManager<ShopManager>().IsInStock(_unit.Index))
                Toggle(true);
        });
    }
    
    private void PlayParticle()
    {
        Vector2 particlePos = GameManager.Instance.GetManager<UIManager>()
            .GetElementPos(_goldElem, new Vector2(0.5f, 0.5f));
        Vector2 destinationPos = GameManager.Instance.GetManager<UIManager>()
            .GetElementPos(_interactionBtn, new Vector2(0.5f, 0.5f));

        PoolableUIMovementParticle particle = GameManager.Instance.GetManager<PoolManager>().Pop("MoneyFeedback") as PoolableUIMovementParticle;
        particle.SetDestination(destinationPos);
        particle.SetPoint(particlePos);
        particle.Play();
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
