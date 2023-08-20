using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradePopup : UIPopup
{
    private VisualElement _exitBtn;

    private VisualElement _boatBtn;
    private VisualElement _fishingBtn;

    private VisualElement _contents;

    private UIBoatUpgradeContent _boatUpgrade;
    private UIFishingUpgradeContent _fishingUpgrade;

    public override void AddEvent()
    {
        _exitBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            RemoveRoot();
        });
        
        _boatBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            _contents.style.right = new StyleLength(new Length(_boatUpgrade.Index * 100, LengthUnit.Percent));
        });

        _fishingBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            _contents.style.right = new StyleLength(new Length(_fishingUpgrade.Index * 100, LengthUnit.Percent));
        });
    }
    
    public override void RemoveEvent()
    {
    }

    public override void FindElement()
    {
        _exitBtn = _root.Q<VisualElement>("exit-btn");

        _boatBtn = _root.Q<VisualElement>("boat-btn");
        _fishingBtn = _root.Q<VisualElement>("fishing-btn");

        _contents = _root.Q<VisualElement>("contents");

        for(int i = 0; i < _contents.childCount; i++){
            VisualElement contentRoot = _contents.ElementAt(i);

            if(contentRoot.name == "boat-content"){
                _boatUpgrade = new UIBoatUpgradeContent(contentRoot, i);
            }
            else{
                _fishingUpgrade = new UIFishingUpgradeContent(contentRoot, i);
            }
        }
    }
}
