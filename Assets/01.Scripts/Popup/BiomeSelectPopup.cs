using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BiomeSelectPopup : UIPopup
{
    private VisualElement _exitBtn;

    private VisualElement _moveRightBtn;
    private VisualElement _moveLeftBtn;

    private VisualElement _contents;

    private List<UIBiomeContent> _biomeContents = new List<UIBiomeContent>();

    private int _currentIndex = 0;

    protected override void AddEvent(VisualElement root)
    {
        _exitBtn.RegisterCallback<ClickEvent>(e => {
            RemoveRoot();
        });

        _moveRightBtn.RegisterCallback<ClickEvent>(e => {
            _currentIndex = Mathf.Clamp(_currentIndex + 1, 0, _contents.childCount);
            _contents.style.right = new StyleLength(new Length(_currentIndex * 100, LengthUnit.Percent));
        });

        _moveLeftBtn.RegisterCallback<ClickEvent>(e => {
            _currentIndex = Mathf.Clamp(_currentIndex - 1, 0, _contents.childCount);
            _contents.style.right = new StyleLength(new Length(_currentIndex * 100, LengthUnit.Percent));
        });
    }

    protected override void FindElement(VisualElement root)
    {
        _exitBtn = root.Q<VisualElement>("exit-btn");

        _moveRightBtn = root.Q<VisualElement>("move-right"); 
        _moveLeftBtn = root.Q<VisualElement>("move-left");

        _contents = root.Q<VisualElement>("contents");
        for(int i = 0; i < _contents.childCount; i++){
            VisualElement biomeElement = _contents.ElementAt(i);
            UIBiomeContent biomeContent = new UIBiomeContent(biomeElement);

            _biomeContents.Add(biomeContent);
        }
    }
}
