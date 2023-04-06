using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 10.0f;
    public float MovementSpeed {
        get => _movementSpeed;
        set {
            if(_navMeshAgent != null)
                _navMeshAgent.speed = value;

            _movementSpeed = value;
        }
    }

    private CharacterController _characterController;
    private NavMeshAgent _navMeshAgent;

    public CharacterController CharacterController => _characterController;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    private void Awake() {
        _characterController = GetComponent<CharacterController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        MovementSpeed = _movementSpeed;
    }

    public void SetDestination(Vector3 target){
        target.y = transform.position.y;
        _navMeshAgent.SetDestination(target);
    }

    public void StopImmediately(){
        SetDestination(transform.position);
    }
}
