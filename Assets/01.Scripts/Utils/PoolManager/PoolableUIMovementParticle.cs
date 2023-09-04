using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableUIMovementParticle : PoolableUIParticle
{
    private RectTransform _destRectTrm;
    private Vector2 _destination;

    public void SetDestination(Vector2 screenPos)
    {
        _destination = screenPos;
        _destRectTrm.anchoredPosition = _destination;
    }

    private void PlayTada()
    {
        PoolableUIParticle tadaEffect = GameManager.Instance.GetManager<PoolManager>().Pop("TadaEffect") as PoolableUIParticle;
        if (tadaEffect != null)
        {
            tadaEffect.SetPoint(_destination);
            tadaEffect.Play();
        }
    }

    public override void Init()
    {
        base.Init();
        _destRectTrm = transform.Find("Target").GetComponent<RectTransform>();
        _particleImage.onParticleFinish.AddListener(PlayTada);
    }
}
