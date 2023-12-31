using Core;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class ChallengeUnit
{
    public Label ChallengeTitle;
    public Label ChallengeInfo;
    public Label ChallengeCurrentState;
    public VisualElement GaugeBar;
    public VisualElement CheckBox;
    public VisualElement TouchBox;
    public ChallengeType Type;
    public int Target;
    public int compensation;
    private int _curState;
    public string TargetFishName;

    private VisualElement _root;

    private int _idx;
    private ChallengeData _data;

    public void Generate(VisualElement root, ChallengeDataUnit dataUnit, int idx)
    {
        _root = root;
        _idx = idx;

        UnityEngine.Debug.Log($"idx: {_idx}");
        _data = (ChallengeData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.ChallengeData);
        
        FindElements();
        Setting(dataUnit);


        TouchBox.RegisterCallback<ClickEvent>(e =>
        {
            if (!_data.units[idx].isReceipt && _data.units[idx].isClear)
            {
                PlayParticle();
                GameManager.Instance.GetManager<MoneyManager>().AddMoney(compensation);
                _data.units[idx].isReceipt = true;
            }
        });
    }
    private void PlayParticle()
    {
        Vector2 particlePos = GameManager.Instance.GetManager<UIManager>()
            .GetElementPos(TouchBox, new Vector2(0.5f, 0.5f));
        Vector2 destinationPos = GameManager.Instance.GetManager<UIManager>()
            .GetElementPos(TouchBox, new Vector2(0.5f, 0.5f));

        PoolableUIMovementParticle particle = GameManager.Instance.GetManager<PoolManager>().Pop("MoneyFeedback") as PoolableUIMovementParticle;
        particle.SetDestination(destinationPos);
        particle.SetPoint(particlePos);
        particle.Play();
    }
    private void Setting(ChallengeDataUnit data)
    {
        //���������Ʈ���� ����/����/��ô��/��ǥ/���� �� �����������

        TargetFishName = data.TargetFishName;
        ChallengeTitle.text = data.Title;
        ChallengeInfo.text = data.Info;
        Type = data.Type;
        switch (Type)
        {
            case ChallengeType.Revenue:
                _curState = _data.units[_idx].totalValue;
                break;
            case ChallengeType.AmountSpent:
                _curState = _data.units[_idx].totalValue;
                break;
            case ChallengeType.FishCount:
                if(TargetFishName == "")
                    _curState = _data.units[_idx].totalValue;
                else
                {
                    if (_data.units[_idx].isClear)
                    {
                        _curState = 1;
                    }
                    else
                    {
                        _curState = 0;
                    }
                }
                break;
            default:
                break;
        }
        ChallengeCurrentState.text = $"{_curState}/{data.Target}";
        Target = data.Target;
        compensation = data.Compensation;
        UnityEngine.Debug.Log($"{data.Title}: {(float)_curState / (float)Target * 100}");
        GaugeBar.style.width = new StyleLength(new Length((float)_curState / (float)Target * 100, LengthUnit.Percent));
        if (_data.units[_idx].isClear)
        {
            CheckBox.style.opacity = new StyleFloat(100);
            GaugeBar.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
        }
    }

    private void FindElements()
    {
        ChallengeTitle = _root.Q<Label>("title-text");
        ChallengeInfo = _root.Q<Label>("info-text");
        ChallengeCurrentState = _root.Q<Label>("current-state-text");

        GaugeBar = _root.Q<VisualElement>("gauge-value");
        CheckBox = _root.Q<VisualElement>("check");
        TouchBox = _root.Q<VisualElement>("touch-box");
    }
}
