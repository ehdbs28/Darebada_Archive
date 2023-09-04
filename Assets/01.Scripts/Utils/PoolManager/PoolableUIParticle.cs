using System.Collections;
using System.Collections.Generic;
using AssetKits.ParticleImage;
using UnityEngine;

public class PoolableUIParticle : PoolableMono
{
    protected ParticleImage _particleImage;
    private RectTransform _rTrm;
    private Coroutine _runningRoutine = null;

    public void SetPoint(Vector2 screenPos)
    {
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
        yield return new WaitForSeconds(_particleImage.main.duration + 0.5f);
        _particleImage.Stop();
        GameManager.Instance.GetManager<PoolManager>().Push(this);
    }
    
    public override void Init()
    {
        _particleImage = GetComponent<ParticleImage>();
        _rTrm = GetComponent<RectTransform>();
    }
}
