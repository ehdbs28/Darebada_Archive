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

    public float Point => _point;
    public float Thickness => _thickness;

    public void SetUp(float point, float thickness){
        _point = point;
        _thickness = thickness;
        
        // _rectTrm.rotation = Quaternion.Euler(0, 0, (_point) - _thickness / 2);
        _rectTrm.rotation = Quaternion.Euler(0, 0, (_point));
        _image.fillAmount = _thickness / 360f;
    }

    public void SetPosition(Vector3 pos){
        _rectTrm.anchoredPosition = pos;
    }

    public void SetScale(Vector3 scale){
        _rectTrm.localScale = scale;
    }

    public override void Init()
    {
        _rectTrm = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }
}
