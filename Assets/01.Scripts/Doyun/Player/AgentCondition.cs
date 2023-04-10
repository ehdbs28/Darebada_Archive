using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentCondition : MonoBehaviour
{
    public event Action<Vector3> OnMouseClickEvent = null;
    public event Action OnOffMeshClimb = null;
    public event Action OnOffMeshJump = null;

    private NavMeshAgent _navMeshAgent;

    [Header("Off Mesh Climb Area")]
    [SerializeField] private int _climbArea;

    [Header("Off Mesh Jump Area")]
    [SerializeField] private int _jumpArea;

    private void Awake() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        MouseClick();
        OnJump();
        OnClimb();
    }

    private void MouseClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // 나중에는 카메라 캐싱해서 사용해야 함
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isHit = Physics.Raycast(ray, out hit, Camera.main.farClipPlane, LayerMask.GetMask("Ground"));

            if (isHit)
            {
                OnMouseClickEvent?.Invoke(hit.point);
            }
        }
    }

    private void OnClimb(){
        if(_navMeshAgent.isOnOffMeshLink){
            OffMeshLinkData linkData = _navMeshAgent.currentOffMeshLinkData;

            if(linkData.offMeshLink != null && linkData.offMeshLink.area == _climbArea){
                OnOffMeshClimb?.Invoke();
            }
        }
    }

    private void OnJump(){
        if(_navMeshAgent.isOnOffMeshLink){
            OffMeshLinkData linkData = _navMeshAgent.currentOffMeshLinkData;

            if(linkData.offMeshLink != null && linkData.offMeshLink.area == _jumpArea){
                OnOffMeshJump?.Invoke();
            }
        }
    }
}
