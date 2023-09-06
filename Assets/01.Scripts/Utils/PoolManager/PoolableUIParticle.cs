using System.Collections;
using System.Collections.Generic;
using AssetKits.ParticleImage;
using UnityEngine;

public class PoolableUIParticle : PoolableMono
{
    protected ParticleImage _particleImage;
    private RectTransform _rTrm;
    private Coroutine _runningRoutine = null;

    private Transform _prevParent = null;

    public void SetPoint(Vector2 screenPos)
    {
        screenPos.y *= -1f;
        _rTrm.anchoredPosition = screenPos;
    }

    public void Play()
    {
        if (_runningRoutine != null)
            return;

        _runningRoutine = StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {
        _particleImage.Play();
        yield return new WaitForSecondsRealtime(_particleImage.main.duration + 0.5f);
        _particleImage.Stop();
        
        if (_prevParent != null)
        {
            transform.SetParent(_prevParent);
            _prevParent = null;
        }
        
        GameManager.Instance.GetManager<PoolManager>().Push(this);
    }
    
    public override void Init()
    {
        _prevParent = transform.parent;
        transform.SetParent(GameManager.Instance.GetManager<UIManager>().Canvas.transform);
        _particleImage = GetComponent<ParticleImage>();
        _rTrm = GetComponent<RectTransform>();
    }
}   
