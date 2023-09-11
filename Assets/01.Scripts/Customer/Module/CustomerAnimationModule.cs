using UnityEngine;

public class CustomerAnimationModule : CommonModule<CustomerController>
{
    private Animator _animator;

    private readonly int _isRunningHash = Animator.StringToHash("IsRunning");

    public override void SetUp(Transform rootTrm)
    {
        base.SetUp(rootTrm);
        _animator = _controller.transform.Find("Visual").GetComponent<Animator>();
    }

    public void SetRunningToggle(bool value)
    {
        _animator.SetBool(_isRunningHash, value);
    }

    public override void UpdateModule()
    {
        
    }

    public override void FixedUpdateModule()
    {
        
    }
}
