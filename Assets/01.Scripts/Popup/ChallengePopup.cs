using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChallengePopup : UIPopup
{
    private List<ChallengeUnit> _challengeList = new List<ChallengeUnit>();

    private VisualElement _exitBtn;
    private VisualElement _unitParent;

    [SerializeField]
    private VisualTreeAsset _unitTemplate;

    public override void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true)
    {
        base.SetUp(document, clearScreen, blur, timeStop);

        var challenges = GameManager.Instance.GetManager<ChallengeManager>().Challenges;
        for (int i = 0; i < challenges.Count; i++)
        {
            VisualElement root = _unitTemplate.Instantiate();
            root = root.Q<VisualElement>("template-container");

            _challengeList.Add(new ChallengeUnit());
            _challengeList[i].Generate(root, challenges[i], i);
            _unitParent.Add(root);
        }
    }

    public override void AddEvent()
    {
        _exitBtn.RegisterCallback<ClickEvent>(e =>
        {
            RemoveRoot();
        });
    }

    public override void FindElement()
    {
        _exitBtn = _root.Q<VisualElement>("exit-btn");
        _unitParent = _root.Q<ScrollView>("challenge-list");
    }

    public override void RemoveEvent()
    {

    }
}
