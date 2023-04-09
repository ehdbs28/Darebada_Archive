using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimator : MonoBehaviour
{
    private readonly int _isWalkHash = Animator.StringToHash("isWalk");
    private readonly int _isGround = Animator.StringToHash("isGround");
    private readonly int _isClimb = Animator.StringToHash("isClimb");

    public event Action OnAnimationEndTrigger = null;

    private Animator _animator;
    public Animator Animator => _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void SetWalkState(bool state){
        _animator.SetBool(_isWalkHash, state);
    }

    public void SetGroundState(bool state){
        _animator.SetBool(_isGround, state);
    }

    public void SetClimbState(bool state){
        _animator.SetBool(_isClimb, state);
    }

    public void OnAnimationEnd(){
        OnAnimationEndTrigger?.Invoke();
    }
}
