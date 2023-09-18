using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingPopup : UIPopup
{
    private VisualElement _exitBtn;
    
    private VisualElement _bgSoundBtn;
    private VisualElement _bgMuteBtn;
    private VisualElement _effectSoundBtn;
    private VisualElement _effectMuteBtn;
    private VisualElement _natureSoundBtn;
    private VisualElement _natureMuteBtn;

    private VisualElement _resetBtn;
    private VisualElement _creditBtn;

    private bool _isBgsOff = false;
    private bool _isSEOff = false;
    private bool _isNSOff = false;

    public override void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true)
    {
        base.SetUp(document, clearScreen, blur, timeStop);

        _isBgsOff = GameManager.Instance.GetManager<SettingManager>().IsBgsOff;
        _isSEOff = GameManager.Instance.GetManager<SettingManager>().IsSEOff;
        _isNSOff = GameManager.Instance.GetManager<SettingManager>().IsNSOff;
        SetState(_bgSoundBtn, _bgMuteBtn, _isBgsOff);
        SetState(_effectSoundBtn, _effectMuteBtn, _isSEOff);
        SetState(_natureSoundBtn, _natureMuteBtn, _isNSOff);
    }

    private void SetState(VisualElement soundBtn, VisualElement muteBtn, bool state)
    {
        if (state)
        {
            soundBtn.AddToClassList("disable");
            muteBtn.RemoveFromClassList("disable");
        }
        else
        {
            soundBtn.RemoveFromClassList("disable");
            muteBtn.AddToClassList("disable");
        }
    }

    public override void AddEvent()
    {
        _exitBtn.RegisterCallback<ClickEvent>(e =>
        {
            GameManager.Instance.GetManager<SettingManager>().IsBgsOff = _isBgsOff;
            GameManager.Instance.GetManager<SettingManager>().IsSEOff = _isSEOff;
            GameManager.Instance.GetManager<SettingManager>().IsNSOff = _isNSOff;
            RemoveRoot();
        });

        _bgSoundBtn.RegisterCallback<ClickEvent>(e =>
        {
            if (_isBgsOff)
            {
                _isBgsOff = false;
                GameManager.Instance.GetManager<SoundManager>().Mute(SoundEnum.BGM, _isBgsOff);
                _bgSoundBtn.RemoveFromClassList("disable");
                _bgMuteBtn.AddToClassList("disable");
            }
        });

        _bgMuteBtn.RegisterCallback<ClickEvent>(e =>
        {
            if(_isBgsOff != true)
            {
                _isBgsOff = true;
                GameManager.Instance.GetManager<SoundManager>().Mute(SoundEnum.BGM, _isBgsOff);
                _bgSoundBtn.AddToClassList("disable");
                _bgMuteBtn.RemoveFromClassList("disable");
            }
        });

        _effectSoundBtn.RegisterCallback<ClickEvent>(e =>
        {
            if (_isSEOff)
            {
                _isSEOff = false;
                GameManager.Instance.GetManager<SoundManager>().Mute(SoundEnum.EFFECT, _isSEOff);
                _effectSoundBtn.RemoveFromClassList("disable");
                _effectMuteBtn.AddToClassList("disable");
            }
        });

        _effectMuteBtn.RegisterCallback<ClickEvent>(e =>
        {
            if(_isSEOff != true)
            {
                _isSEOff = true;
                GameManager.Instance.GetManager<SoundManager>().Mute(SoundEnum.EFFECT, _isSEOff);
                _effectSoundBtn.AddToClassList("disable");
                _effectMuteBtn.RemoveFromClassList("disable");
            }
        });

        _natureSoundBtn.RegisterCallback<ClickEvent>(e =>
        {
            if (_isNSOff)
            {
                _isNSOff = false;
                // GameManager.Instance.GetManager<SoundManager>().Mute(SoundEnum.EFFECT, _isSEOff);
                _natureSoundBtn.RemoveFromClassList("disable");
                _natureMuteBtn.AddToClassList("disable");
            }
        });

        _natureMuteBtn.RegisterCallback<ClickEvent>(e =>
        {
            if(_isNSOff != true)
            {
                _isNSOff = true;
                // GameManager.Instance.GetManager<SoundManager>().Mute(SoundEnum.EFFECT, _isSEOff);
                _natureSoundBtn.AddToClassList("disable");
                _natureMuteBtn.RemoveFromClassList("disable");
            }
        });

        _resetBtn.RegisterCallback<ClickEvent>(e =>
        {
            ((DataRemoveCheckPopup)GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.DataRemoveCheck, false))
                .SetBasePopup(this);
        });

        _creditBtn.RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log("ũ���� ���");
        });
    }

    public override void FindElement()
    {
        _exitBtn = _root.Q<VisualElement>("exit-btn");

        _bgSoundBtn = _root.Q<VisualElement>("background-sound").Q<VisualElement>("sound-btn");
        _bgMuteBtn = _root.Q<VisualElement>("background-sound").Q<VisualElement>("mute-btn");
        _effectSoundBtn = _root.Q<VisualElement>("effect-sound").Q<VisualElement>("sound-btn");
        _effectMuteBtn = _root.Q<VisualElement>("effect-sound").Q<VisualElement>("mute-btn");
        _natureSoundBtn = _root.Q<VisualElement>("nature-sound").Q<VisualElement>("sound-btn");
        _natureMuteBtn = _root.Q<VisualElement>("nature-sound").Q<VisualElement>("mute-btn");

        _resetBtn = _root.Q<VisualElement>("data-reset-btn");
        _creditBtn = _root.Q<VisualElement>("show-credit-btn");
    }

    public override void RemoveEvent()
    {

    }
}
