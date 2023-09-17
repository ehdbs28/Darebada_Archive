using UnityEngine;
using UnityEngine.UIElements;

public class UIBowlUpgradeContent
{
    private VisualElement _root;
    private Fishbowl _fishbowl;

    private Label _goldLabel;
    private VisualElement _upgradeBtn;

    public UIBowlUpgradeContent(VisualElement root, Fishbowl fishbowl)
    {
        _root = root;
        _fishbowl = fishbowl;
        
        FindElement();
        AddEvent();
        OnGoldChanged((GameManager.Instance.GetManager<DataManager>().GetData(DataType.GameData) as GameData).HoldingGold);
    }

    private void FindElement()
    {
        _upgradeBtn = _root.Q("upgrade-btn");
        _goldLabel = _root.Q<Label>("gold-text");
    }
    
    private void OnGoldChanged(int gold)
    {
        _goldLabel.text = $"{gold:N}";
    }

    private void AddEvent()
    {
        GameManager.Instance.GetManager<MoneyManager>().OnGoldChange += OnGoldChanged;
        
        _upgradeBtn.RegisterCallback<ClickEvent>(e =>
        {
            GameManager.Instance.GetManager<MoneyManager>().Payment(1000, () =>
            {
                _fishbowl.Upgrade();
                PlayParticle();
            });
        });
    }

    private void PlayParticle()
    {
        var particlePos = GameManager.Instance.GetManager<UIManager>()
            .GetElementPos(_goldLabel, new Vector2(0.5f, 0.5f));
        var destPos = GameManager.Instance.GetManager<UIManager>()
            .GetElementPos(_upgradeBtn, new Vector2(0.5f, 0.5f));

        var particle =
            GameManager.Instance.GetManager<PoolManager>().Pop("MoneyFeedback") as PoolableUIMovementParticle;
        particle.SetDestination(destPos);
        particle.SetPoint(particlePos);
        particle.Play();
    }
}
