using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishIcon : MonoBehaviour
{
    public FishInfoSO fishInfo;
    private Image _image;
    private void OnEnable()
    {
        _image= GetComponent<Image>();
        Show();
    }
    private void Update()
    {

    }
    public void OpenPanel()
    {
        InfoPanel panel = FindObjectOfType<InfoPanel>();
        if(!panel.gameObject.activeSelf)
        {
            panel.fishSO= fishInfo;
            panel.gameObject.SetActive(true);
        }
    }
    public void SetDonate(bool state)
    {
        fishInfo.hadDonated = state;
        Show();
    }

    public void Show()
    {
        Debug.Log(fishInfo.hadDonated);
        if(fishInfo.hadDonated)
        {
            _image.sprite = fishInfo.icon;
        }else
        {
            _image.sprite = fishInfo.obscuredIcon;
        }
    }
}
