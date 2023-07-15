using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameAnswer : PoolableMono
{
    [SerializeField]
    private float _point;

    [SerializeField]
    private float _thickness;
    
    private RectTransform _rectTrm;
    private Image _image;

    public void SetAnswerPoint(float point, float thickness){
        _point = point;
        _thickness = thickness;
        
        _rectTrm.rotation = Quaternion.Euler(0, 0, _point - _thickness / 2 - 180f);
        _image.fillAmount = _thickness / 360f;
    }

    public override void Init()
    {
        _rectTrm = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }
}
