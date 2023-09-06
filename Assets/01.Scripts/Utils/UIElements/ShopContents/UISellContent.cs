using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UISellContent : UIPopupContent
{
    private VisualElement _sellBtn;

    private Label _nameText;
    private Label _priceText;

    private VisualElement _fishImage;

    public UISellContent(VisualElement root, int index) : base(root, index)
    {
    }

    protected override void FindElement()
    {
        base.FindElement();
        _sellBtn = _root.Q("sell-btn");

        _nameText = _root.Q<Label>("name-text");
        _priceText = _root.Q<Label>("price-text");

        _fishImage = _root.Q("fish-image");
    }

    protected override void AddEvent()
    {
        base.AddEvent();

        _sellBtn.RegisterCallback<ClickEvent>(e => {
            Debug.Log("판매 완료");
            PlayParticle();    
        });
    }

    private void PlayParticle()
    {
        Vector2 particlePos = GameManager.Instance.GetManager<UIManager>()
            .GetElementPos(_sellBtn, new Vector2(0.5f, 0.5f));
        // Vector2 destinationPos = GameManager.Instance.GetManager<UIManager>()
        //     .GetElementPos()

        PoolableUIMovementParticle particle = GameManager.Instance.GetManager<PoolManager>().Pop("MoneyFeedback") as PoolableUIMovementParticle;
        particle.SetPoint(particlePos);
        particle.Play();
    }
}
