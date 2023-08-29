using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopPopup : UIPopup
{
    private VisualElement _exitBtn;

    private VisualElement _sellBtn;
    private VisualElement _buyBtn;

    private VisualElement _contents;

    private UISellContent _sellContent;
    private UIBuyContent _buyContent;

    public override void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true)
    {
        base.SetUp(document, clearScreen, blur, timeStop);
    }

    public override void AddEvent()
    {
        _exitBtn.RegisterCallback<ClickEvent>(e => {
            RemoveRoot();
        });

        _sellBtn.RegisterCallback<ClickEvent>(e => {
            _contents.style.right = new StyleLength(new Length(_sellContent.Index * 100, LengthUnit.Percent));
        });

        _buyBtn.RegisterCallback<ClickEvent>(e => {
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

            Debug.Log(contentRoot.name);

            if(contentRoot.name == "sell-content"){
                _sellContent = new UISellContent(contentRoot, i);
            }
            else{
                _buyContent = new UIBuyContent(contentRoot, i);
            }
        }
    }
}
