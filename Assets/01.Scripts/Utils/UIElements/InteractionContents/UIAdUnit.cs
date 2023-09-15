using UnityEngine;
using UnityEngine.UIElements;

public class UIAdUnit : UIInteractionElement
{
    private int _price;
    private int _value;

    private VisualElement _gold;
    
    public UIAdUnit(VisualElement root, VisualElement gold, int price, int value) : base(root)
    {
        _price = price;
        _value = value;
        _gold = gold;
        
        FindElement();
        AddEvent();
    }

    protected override void AddEvent()
    {
        _interactionBtn.RegisterCallback<ClickEvent>(e =>
        {
            GameManager.Instance.GetManager<MoneyManager>().Payment(_price, () =>
            {
                AquariumManager.Instance.Promotion(_value);
                PlayParticle();
            });
        });
    }
    
    protected override void FindElement()
    {
        _interactionBtn = _root.Q("buy-btn");
    }

    private void PlayParticle()
    {
        var dest = GameManager.Instance.GetManager<UIManager>().GetElementPos(_interactionBtn, new Vector2(0.5f, 0.5f));
        var particlePos = GameManager.Instance.GetManager<UIManager>()
            .GetElementPos(_gold, new Vector2(0.5f, 0.5f));
        var particle = GameManager.Instance.GetManager<PoolManager>().Pop("MoneyFeedback") as PoolableUIMovementParticle;
        particle.SetDestination(dest);
        particle.SetPoint(particlePos);
        particle.Play();
    }
}
