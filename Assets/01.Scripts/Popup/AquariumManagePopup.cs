using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AquariumManagePopup : UIPopup
{
    private VisualElement _exitBtn;
    private VisualElement _contentPanel;

    private VisualElement _cleanBtn;
    private VisualElement _adBtn;

    private UICleanContent _cleanContent;
    private UIAdContent _adContent;

    public override void AddEvent()
    {
        _exitBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            RemoveRoot();
        });
        
        _cleanBtn.RegisterCallback<ClickEvent>(e =>
        {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            _contentPanel.style.right = new StyleLength(new Length(_cleanContent.Index * 100, LengthUnit.Percent));
        });
        
        _adBtn.RegisterCallback<ClickEvent>(e =>
        {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            _contentPanel.style.right = new StyleLength(new Length(_adContent.Index * 100, LengthUnit.Percent));
        });
    }

    public override void RemoveEvent()
    {
    }

    public override void FindElement()
    {
        _exitBtn = _root.Q<VisualElement>("exit-btn");
        _contentPanel = _root.Q("contents");
        _cleanBtn = _root.Q("clean-btn");
        _adBtn = _root.Q("ad-btn");

        for (int i = 0; i < _contentPanel.childCount; i++)
        {
            var contentRoot = _contentPanel.ElementAt(i);
            if (contentRoot.name == "clean-content")
            {
                _cleanContent = new UICleanContent(contentRoot, i);
            }
            else if (contentRoot.name == "ad-content")
            {
                _adContent = new UIAdContent(contentRoot, i);
            }
        }
    }
}
