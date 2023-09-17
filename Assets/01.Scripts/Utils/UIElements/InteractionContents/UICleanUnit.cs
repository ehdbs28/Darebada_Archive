using UnityEngine;
using UnityEngine.UIElements;

public class UICleanUnit : UIInteractionElement
{
    private int _price;
    private int _cleaningValue;

    private VisualElement _goldElem;

    public UICleanUnit(VisualElement root, VisualElement goldElem, int price, int cleaningValue) : base(root)
    {
        _price = price;
        _cleaningValue = cleaningValue;

        _goldElem = goldElem;
        
        FindElement();
        AddEvent();
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

    protected override void AddEvent()
    {
        _interactionBtn.RegisterCallback<ClickEvent>(e =>
        {
            GameManager.Instance.GetManager<MoneyManager>().Payment(_price, () =>
            {
                PlayParticle();
                AquariumManager.Instance.Cleaning(_cleaningValue);
            });
        });
    }

    protected override void FindElement()
    {
        _interactionBtn = _root.Q("buy-btn");
    }
}
