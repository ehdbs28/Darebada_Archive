using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FishingScreen : UIScreen
{
    private Label _heightText;
    private VisualElement _heightCursor;

    private VisualElement _reelUpBtn;

    public event Action ReelUpBtnClickEvent = null;

    private bool _isBtnClick = false;

    public override void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true)
    {
        base.SetUp(document, clearScreen, blur, timeStop);
        _isBtnClick = false;
    }

    public override void AddEvent()
    {
        _reelUpBtn.RegisterCallback<MouseDownEvent>(e =>
        {
            _isBtnClick = true;
        });
        
        _reelUpBtn.RegisterCallback<MouseUpEvent>(e =>
        {
            OnTouchUp();
        });

        GameManager.Instance.GetManager<InputManager>().OnTouchUpEvent += OnTouchUp;
    }

    private void Update()
    {
        if (_isBtnClick)
        {
            ReelUpBtnClickEvent?.Invoke();
        }
    }

    private void OnTouchUp()
    {
        if (_isBtnClick)
        {
            _isBtnClick = false;
        }
    }

    public override void RemoveEvent()
    {
        GameManager.Instance.GetManager<InputManager>().OnTouchUpEvent -= OnTouchUp;
    }

    public override void FindElement()
    {
        _heightText = _root.Q<Label>("height-text");
        _heightCursor = _root.Q<VisualElement>("cursor");
        _reelUpBtn = _root.Q<VisualElement>("reel-up-btn");
    }

    public void SetHeight(float percent, float height){
        _heightCursor.style.top = new StyleLength(new Length(percent * 100, LengthUnit.Percent));
        _heightText.text = $"{(int)height}M";
    }
}
