using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UISellContent : UIPopupContent
{
    private VisualElement _sellBtn;

    private VisualElement _invenContent;

    private Label _nameText;
    private Label _priceText;

    private VisualElement _fishImage;

    private InventoryUnit _selectUnit = null;
    private InventoryUnit selectUnit
    {
        get => _selectUnit;
        set
        {
            _selectUnit = value;

            if (_selectUnit != null)
            {
                _fishImage.style.backgroundImage = new StyleBackground(_selectUnit.data.Visual.Profile);
                _nameText.text = _selectUnit.data.Name;
                _priceText.text = $"판매가격: {_selectUnit.data.Price:N}";
            }
            else
            {
                _fishImage.style.backgroundImage = null;
                _nameText.text = "";
                _priceText.text = "";
            }
        }
    }

    public UISellContent(VisualTreeAsset unitTemplete, VisualElement root, int index) : base(root, index)
    {
        selectUnit = null;

        var invenData = (InventoryData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.InventoryData);
        invenData.Units.List.ForEach(unit =>
        {
            VisualElement root = unitTemplete.Instantiate();
            root = root.Q("inventory-unit");
            unit.Generate(root, true, true);
            _invenContent.Add(root);
            
            unit.Root.RegisterCallback<ClickEvent>(evt =>
            {
                selectUnit = unit;
            });
        });
    }

    protected override void FindElement()
    {
        base.FindElement();
        _sellBtn = _root.Q("sell-btn");

        _invenContent = _root.Q("contents");

        _nameText = _root.Q<Label>("name-text");
        _priceText = _root.Q<Label>("price-text");

        _fishImage = _root.Q("fish-image");
    }

    protected override void AddEvent()
    {
        base.AddEvent();

        _sellBtn.RegisterCallback<ClickEvent>(e =>
        {
            if (_selectUnit == null)
                return;
            
            GameManager.Instance.GetManager<MoneyManager>().AddMoney(_selectUnit.data.Price);
            (GameManager.Instance.GetManager<DataManager>().GetData(DataType.InventoryData) as InventoryData)
                ?.Units.List.Remove(_selectUnit);
            _invenContent.Remove(_selectUnit.Root);
            
            _selectUnit = null;
            
            PlayParticle();    
        });
    }

    private void PlayParticle()
    {
        Vector2 particlePos = GameManager.Instance.GetManager<UIManager>()
            .GetElementPos(_goldLabel, new Vector2(0.5f, 0.5f));
        Vector2 destinationPos = GameManager.Instance.GetManager<UIManager>()
            .GetElementPos(_sellBtn, new Vector2(0.5f, 0.5f));

        PoolableUIMovementParticle particle = GameManager.Instance.GetManager<PoolManager>().Pop("MoneyFeedback") as PoolableUIMovementParticle;
        particle.SetPoint(particlePos);
        particle.Play();
    }
}
