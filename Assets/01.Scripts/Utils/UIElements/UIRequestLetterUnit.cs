using UnityEngine;
using UnityEngine.UIElements;

public class UIRequestLetterUnit : UILetterUnit
{
    private VisualElement _acceptBtn;
    private bool _canAccept;
    
    public UIRequestLetterUnit(VisualTreeAsset templete, ScrollView parent) : base(templete, parent)
    {
        // 나중에 도전과제 매니저에서 가져오자
        _canAccept = true;
    }

    public override void Generate(LetterUnit unit)
    {
        base.Generate(unit);
        _acceptBtn.style.opacity = _canAccept ? 1f : 0f;
    }

    protected override void FindElement()
    {
        base.FindElement();
        _acceptBtn = _root.Q("request-accept-btn");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        _acceptBtn.RegisterCallback<ClickEvent>(e =>
        {
            if (_canAccept)
            {
                _canAccept = false;
                PlayParticle();
                Debug.Log("도전과제 수락");
                _acceptBtn.style.opacity = 0.5f;
            }
            e.StopPropagation();
        });
    }

    private void PlayParticle()
    {
        Vector2 particlePos = GameManager.Instance.GetManager<UIManager>().GetElementPos(_acceptBtn, new Vector2(0.5f, 0.5f));
        PoolableUIParticle particle = GameManager.Instance.GetManager<PoolManager>().Pop("TadaEffect") as PoolableUIParticle;
        particle.SetPoint(particlePos);
        particle.Play();
    }
}
