using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickerMovementModule : CommonModule<PlayerController>
{
    private NavMeshAgent _navAgent;

    public override void SetUp(Transform rootTrm)
    {
        base.SetUp(rootTrm);

        _navAgent = rootTrm.GetComponent<NavMeshAgent>();
        _navAgent.speed = _controller.GetModule<PlayableMovementModule>().MaxSpeed;

        GameManager.Instance.GetManager<InputManager>().OnMouseClickEvent += OnMouseClick;
    }

    public override void FixedUpdateModule()
    {
    }

    public override void UpdateModule()
    {
    }

    private void OnMouseClick(bool value){
        if(value && _controller.ActionData.PlayerState == PlayerState.CLICKER){
            Movement(GameManager.Instance.GetManager<InputManager>().MousePositionToGroundRayPostion);
        }
    }

    private void Movement(Vector3 dest){
        _navAgent.SetDestination(dest);
    }

    private void StopImmedietely(){
        Movement(transform.position);
    }
}