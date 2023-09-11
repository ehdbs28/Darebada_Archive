using UnityEngine;
using UnityEngine.AI;

public class CustomerMovementModule : CommonModule<CustomerController>
{
    [SerializeField]
    private float _movementSpeed;
    
    [SerializeField]
    private float _movementTime = 2f;
    
    [SerializeField]
    private float _waitTime = 2f;

    private float _lastMoveTime;
    private float _movementDelay;
    private float _waitDelay;

    public override void SetUp(Transform rootTrm)
    {
        base.SetUp(rootTrm);

        _controller.transform.position = GetRandomPosOnNav();

        GameManager.Instance.GetManager<TimeManager>().OnTimeChangedEvent += OnTimeChanged;
    }

    private void OnTimeChanged(int hour, int minute, float currentTime)
    {
        // arrived on destination
        if (currentTime >= _lastMoveTime + _movementDelay)
        {
            _controller.GetModule<CustomerAnimationModule>().SetRunningToggle(false);
            // wait time
            if (currentTime >= _lastMoveTime + _movementDelay + _waitDelay)
            {
                Vector3 randomPos = GetRandomPosOnNav();
                _controller.NavAgent.SetDestination(randomPos);
                
                _lastMoveTime = currentTime;
                _movementDelay = _movementTime + Random.Range(0.2f, 2f);
                _waitDelay = _waitTime + Random.Range(0.2f, 2f);
                
                _controller.GetModule<CustomerAnimationModule>().SetRunningToggle(true);
            }
        }
    }

    private Vector3 GetRandomPosOnNav()
    {
        Vector3 randomDir = Random.insideUnitSphere * 20f;
        randomDir += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDir, out hit, 20f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            return transform.position;
        }
    }

    public override void UpdateModule()
    {
        
    }

    public override void FixedUpdateModule()
    {
        
    }
}
