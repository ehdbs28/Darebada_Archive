using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BiomeSelectPopup : UIPopup
{
    private VisualElement _exitBtn;

    private VisualElement _moveRightBtn;
    private VisualElement _moveLeftBtn;
    private Label _biomeNameLabel;

    private VisualElement _contents;

    private List<UIBiomeContent> _biomeContents = new List<UIBiomeContent>();

    private int _currentIndex = 0;

    public override void AddEvent()
    {
        _exitBtn.RegisterCallback<ClickEvent>(e => {
            RemoveRoot();
        });

        _moveRightBtn.RegisterCallback<ClickEvent>(e => {
            _currentIndex = Mathf.Clamp(_currentIndex + 1, 0, _contents.childCount - 1);
            ChangeName((OceanType)_currentIndex);
            _contents.style.right = new StyleLength(new Length(_currentIndex * 100, LengthUnit.Percent));
        });

        _moveLeftBtn.RegisterCallback<ClickEvent>(e => {
            _currentIndex = Mathf.Clamp(_currentIndex - 1, 0, _contents.childCount - 1);
            ChangeName((OceanType)_currentIndex);
            _contents.style.right = new StyleLength(new Length(_currentIndex * 100, LengthUnit.Percent));
        });
    }

    private void ChangeName(OceanType type){
        switch(type){
            case OceanType.RichOcean:
                _biomeNameLabel.text = "풍요의 바다";
            break;
            case OceanType.SouthOcean:
                _biomeNameLabel.text = "남쪽의 바다";
            break;
            case OceanType.RainyOcean:
                _biomeNameLabel.text = "비의 바다";
            break;
            case OceanType.ColdOcean:
                _biomeNameLabel.text = "추위의 바다";
            break;
            case OceanType.SilenceOcean:
                _biomeNameLabel.text = "고요의 바다";
            break;
        }
    }
    
    public override void RemoveEvent()
    {
    }

    public override void FindElement()
    {
        _exitBtn = _root.Q<VisualElement>("exit-btn");

        _moveRightBtn = _root.Q<VisualElement>("move-right"); 
        _moveLeftBtn = _root.Q<VisualElement>("move-left");

        _biomeNameLabel = _root.Q<Label>("biome-name");

        _contents = _root.Q<VisualElement>("contents");
        for(int i = 0; i < _contents.childCount; i++){
            VisualElement biomeElement = _contents.ElementAt(i);
            UIBiomeContent biomeContent = new UIBiomeContent(biomeElement, this, (OceanType)i);
            _biomeContents.Add(biomeContent);
        }
    }
}
