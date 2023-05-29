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

    protected override void AddEvent(VisualElement root)
    {
        _exitBtn.RegisterCallback<ClickEvent>(e => {
            RemoveRoot();
        });
        
        _boatBtn.RegisterCallback<ClickEvent>(e => {
            _contents.style.right = new StyleLength(new Length(_boatUpgrade.Index * 100, LengthUnit.Percent));
        });

        _fishingBtn.RegisterCallback<ClickEvent>(e => {
            _contents.style.right = new StyleLength(new Length(_fishingUpgrade.Index * 100, LengthUnit.Percent));
        });
    }

    protected override void FindElement(VisualElement root)
    {
        _exitBtn = root.Q<VisualElement>("exit-btn");

        _boatBtn = root.Q<VisualElement>("boat-btn");
        _fishingBtn = root.Q<VisualElement>("fishing-btn");

        _contents = root.Q<VisualElement>("contents");

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
