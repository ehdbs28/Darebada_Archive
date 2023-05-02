using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishIcon : MonoBehaviour
{
    public FishInfoSO fishInfo;
    private Image _image;
    private InfoPanel _panel;
    private void Awake()
    {
        _panel = FindObjectOfType<InfoPanel>();
    }
    private void OnEnable()
    {
        _image= GetComponent<Image>();
        Show();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) SetDonate(!fishInfo.hadDonated);
    }
    public void OpenPanel()
    {
        if(!_panel.gameObject.activeSelf)
        {
            FishDexManager.Instance.OpenDex(fishInfo);
        }
    }
    public void SetDonate(bool state)
    {
        fishInfo.hadDonated = state;
        Show();
    }

    public void Show()
    {
        if(fishInfo.hadDonated)
        {
            _image.sprite = fishInfo.icon;
        }else
        {
            _image.sprite = fishInfo.obscuredIcon;
        }
    }
}
