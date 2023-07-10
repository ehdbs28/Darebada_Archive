using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void SetBool(string name, bool value)
    {
        _animator.SetBool(name, value);
    }
    public void SetTrigger(string name)
    {
        _animator.SetTrigger(name);
    }
    public void SetInt(string name, int value)
    {
        _animator.SetInteger(name, value);
    }
    public void SetFloat(string name, float value)
    {
        _animator.SetFloat(name, value);
    }
}
