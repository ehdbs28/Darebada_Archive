using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentCondition : MonoBehaviour
{
    public event Action<Vector3> OnMouseClickEvent = null;
    public event Action OnOffMeshClimb = null;

    private NavMeshAgent _navMeshAgent;

    [Header("Off Mesh Climb Area")]
    [SerializeField] private int _climbArea;

    private void Awake() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        MouseClick();
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
            Debug.Log(linkData);

            if(linkData.offMeshLink != null && linkData.offMeshLink.area == _climbArea){
                OnOffMeshClimb?.Invoke();
            }
        }
    }
}
