using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingAnimationController : MonoBehaviour
{
    private Animator _animator;

    private readonly int _castingTriggerHash = Animator.StringToHash("casting");

    public event Action OnAnimationEvent = null;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void SetCasting(bool value){
        if(value){
            _animator.SetTrigger(_castingTriggerHash);
        }
        else{
            _animator.ResetTrigger(_castingTriggerHash);
        }
    }

    public void OnAnimation(){
        OnAnimationEvent?.Invoke();
    }
}
