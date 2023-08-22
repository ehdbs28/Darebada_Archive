using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableParticle : PoolableMono
{
    private ParticleSystem _particleSystem;
    private Coroutine _runningRoutine = null;

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }

    public void Play()
    {
        if (_runningRoutine != null)
            return;

        _runningRoutine = StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {
        _particleSystem.Play();
        yield return new WaitForSeconds(_particleSystem.main.duration + 0.5f);
        _particleSystem.Stop();
        GameManager.Instance.GetComponent<PoolManager>().Push(this);
    }
    
    public override void Init()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
}
