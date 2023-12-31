using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopPopup : UIPopup
{
    [SerializeField] private VisualTreeAsset _invenUnitTemplete;
    
    private VisualElement _exitBtn;

    private VisualElement _sellBtn;
    private VisualElement _buyBtn;

    private VisualElement _contents;

    private UISellContent _sellContent;
    private UIBuyContent _buyContent;

    public override void AddEvent()
    {
        _exitBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            RemoveRoot();
        });

        _sellBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            _contents.style.right = new StyleLength(new Length(_sellContent.Index * 100, LengthUnit.Percent));
        });

        _buyBtn.RegisterCallback<ClickEvent>(e => {
            GameManager.Instance.GetManager<SoundManager>().ClickSound();
            _contents.style.right = new StyleLength(new Length(_buyContent.Index * 100, LengthUnit.Percent));
        });
    }

    public override void RemoveEvent()
    {
    }

    public override void FindElement()
    {
        _exitBtn = _root.Q<VisualElement>("exit-btn");

        _sellBtn = _root.Q<VisualElement>("sell-btn");
        _buyBtn = _root.Q<VisualElement>("buy-btn");

        _contents = _root.Q<VisualElement>("contents");

        for(int i = 0; i < _contents.childCount; i++){
            VisualElement contentRoot = _contents.ElementAt(i);

            if(i == 0){
                _sellContent = new UISellContent(_invenUnitTemplete, contentRoot, i);
            }
            else{
                _buyContent = new UIBuyContent(contentRoot, i);
            }
        }
    }
}
