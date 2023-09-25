using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableUIMovementParticle : PoolableUIParticle
{
    private Vector2 _destination;

    public void SetDestination(Vector2 screenPos)
    {
        _destination = screenPos;
        _destination.y *= -1;
        GameManager.Instance.GetManager<UIManager>().DestinationRectTrm.anchoredPosition = _destination;
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
        _particleImage.attractorTarget = GameManager.Instance.GetManager<UIManager>().DestinationRectTrm;
        _particleImage.onParticleFinish.AddListener(PlayTada);
    }
}
