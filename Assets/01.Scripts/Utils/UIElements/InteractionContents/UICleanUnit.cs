using AssetKits.ParticleImage.Editor;
using UnityEngine;
using UnityEngine.UIElements;

public class UICleanUnit : UIInteractionElement
{
    private int _price;
    private int _cleaningValue;

    public UICleanUnit(VisualElement root, int price, int cleaningValue) : base(root)
    {
        _price = price;
        _cleaningValue = cleaningValue;
        
        FindElement();
        AddEvent();
    }

    protected override void AddEvent()
    {
        _interactionBtn.RegisterCallback<ClickEvent>(e =>
        {
            GameManager.Instance.GetManager<MoneyManager>().Payment(_price, () =>
            {
                AquariumManager.Instance.Cleaning(_cleaningValue);
            });
        });
    }

    protected override void FindElement()
    {
        _interactionBtn = _root.Q("buy-btn");
    }
}
