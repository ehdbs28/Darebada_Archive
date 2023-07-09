using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LetterPopup : UIPopup
{
    private VisualElement _exitBtn;
    private VisualElement _deleteBtn;
    private VisualElement _selectAllToggle;

    private ScrollView _letterPerent;

    private List<UILetterUnit> _letters = new List<UILetterUnit>();

    [SerializeField]
    private VisualTreeAsset _letterTemplete;

    public override void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true)
    {
        _isOpenPopup = true;

        _documentRoot = document.rootVisualElement.Q("main-container");

        if(clearScreen && _documentRoot.childCount >= 2){
            for(int i = 0; i < _documentRoot.childCount; i++){
                if(_documentRoot.ElementAt(i).ClassListContains("blur-panel")) continue;
                _documentRoot.RemoveAt(i);
            }
        }

        if(timeStop)
            GameManager.Instance.GetManager<TimeManager>().TimeScale = 0f;

        if(blur){
            _blurPanel = _documentRoot.Q(className: "blur-panel");
            _blurPanel.AddToClassList("on");
        }

        GenerateRoot();
        GenerateLetterUnit();

        if(_root != null){
            AddEvent(_root);
            _documentRoot.Add(_root);
        }
    }

    private void GenerateLetterUnit(){
        List<LetterUnit> letterUnits = GameManager.Instance.GetManager<LetterManager>().Letters;
        for(int i = 0; i < letterUnits.Count; i++){
            _letters.Add(new UILetterUnit(_letterTemplete, _letterPerent));
            _letters[i].Generate(letterUnits[i]);
        }
    }

    protected override void AddEvent(VisualElement root)
    {
        _exitBtn.RegisterCallback<ClickEvent>(e => {
            RemoveRoot();
        });

        _selectAllToggle.RegisterCallback<ClickEvent>(e =>
        {
            foreach(UILetterUnit letter in _letters)
            {
                letter.Toggle(true);
            }
        });

        _deleteBtn.RegisterCallback<ClickEvent>(e =>
        {
            foreach(UILetterUnit letter in _letters)
            {
                if (letter.IsOpen)
                {
                    letter.Remove();
                }
            }
        });
    }
    
    public override void RemoveEvent()
    {
    }

    protected override void FindElement(VisualElement root)
    {
        _exitBtn = root.Q<VisualElement>("exit-btn");
        _deleteBtn = root.Q<VisualElement>("delete-btn");
        _selectAllToggle = root.Q<VisualElement>("select-all-toggle").Q<VisualElement>("inner");
        _letterPerent = root.Q<ScrollView>("content-scroll");
    }
}
