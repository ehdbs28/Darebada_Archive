using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TankUpgradePopup : UIPopup
{
    [SerializeField] private VisualTreeAsset _inventoryUnitTemplete;
    
    private Fishbowl _fishbowl;

    private VisualElement _contents;

    private VisualElement _bowlUpgradeBtn;
    private VisualElement _fishAddBtn;
    private VisualElement _decoBtn;
    private VisualElement _fishDeleteBtn;

    private VisualElement _exitBtn;

    private UIBowlUpgradeContent _bowlUpgradeContent;
    private UIFishAddContent _fishAddContent;
    private UIDecoContent _decoContent;
    private UIFishDeleteContent _fishDeleteContent;

    public override void AddEvent()
    {
        _exitBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            RemoveRoot();
        });
        
        _bowlUpgradeBtn.RegisterCallback<ClickEvent>(e =>
        {
            _contents.style.right = new StyleLength(new Length(0, LengthUnit.Percent));
        });
        _fishAddBtn.RegisterCallback<ClickEvent>(e =>
        {
            _contents.style.right = new StyleLength(new Length(100, LengthUnit.Percent));
        });
        _decoBtn.RegisterCallback<ClickEvent>(e =>
        {
            _contents.style.right = new StyleLength(new Length(200, LengthUnit.Percent));
        });
        _fishDeleteBtn.RegisterCallback<ClickEvent>(e =>
        {
            _contents.style.right = new StyleLength(new Length(300, LengthUnit.Percent));
        });
    }

    public override void RemoveEvent()
    {
    }

    public override void FindElement()
    {
        _exitBtn = _root.Q<VisualElement>("exit-btn");
        _contents = _root.Q("contents");
        _bowlUpgradeBtn = _root.Q("fishbowl-btn");
        _fishAddBtn = _root.Q("addfish-btn");
        _decoBtn = _root.Q("deco-btn");
        _fishDeleteBtn = _root.Q("removefish-btn");

        VisualElement bowlUpgradeRoot = _contents.Q("fishbowl-content");
        _bowlUpgradeContent = new UIBowlUpgradeContent(this, bowlUpgradeRoot, _fishbowl);
        
        VisualElement fishAddRoot = _contents.Q("addfish-content");
        _fishAddContent = new UIFishAddContent(fishAddRoot, _inventoryUnitTemplete, _fishbowl);
            
        VisualElement decoRoot = _contents.Q("deco-content");
        _decoContent = new UIDecoContent(decoRoot, _fishbowl);
        
        VisualElement fishDeleteRoot = _contents.Q("removefish-content");
        _fishDeleteContent = new UIFishDeleteContent(fishDeleteRoot, _fishbowl);
    }

    public void SetFishBowl(Fishbowl fishbowl)
    {
        _fishbowl = fishbowl;
    }
}
