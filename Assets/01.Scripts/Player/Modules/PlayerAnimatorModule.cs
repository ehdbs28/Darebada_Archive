using UnityEngine;

public class PlayerAnimatorModule : CommonModule<PlayerController>
{
    private Animator _animator;

    private readonly int _runningToggleHash = Animator.StringToHash("IsRunning");

    public override void SetUp(Transform rootTrm)
    {
        base.SetUp(rootTrm);
        _animator = rootTrm.Find("Visual").GetComponent<Animator>();
    }

    public void RunningToggle(bool value)
    {
        _animator.SetBool(_runningToggleHash, value);
    }

    public override void UpdateModule()
    {
    }

    public override void FixedUpdateModule()
    {
    }
}
