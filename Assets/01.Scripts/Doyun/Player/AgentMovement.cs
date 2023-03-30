using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.5f;

    private AgentInput _agentInput;
    private IEnumerator _currentRunCoroutine = null;

    private void Awake()
    {
        _agentInput = GetComponent<AgentInput>();
    }

    private void Start()
    {
        _agentInput.OnMouseClickEvent += OnMove;
        _agentInput.OnMouseClickEvent += OnRotate;
    }

    private void OnMove(Vector3 mousePos)
    {
        DOMove(transform.position, mousePos, _moveSpeed);
    }

    private void OnRotate(Vector3 mousePos)
    {
        Vector3 lookPos = mousePos;
        lookPos.y = 0;

        transform.LookAt(lookPos);
    }

    private void DOMove(Vector3 start, Vector3 end, float duration)
    {
        if(_currentRunCoroutine != null)
        {
            StopCoroutine(_currentRunCoroutine);
        }

        _currentRunCoroutine = DOMoveCoroutine(start, end, duration);
        StartCoroutine(_currentRunCoroutine);
    }

    IEnumerator DOMoveCoroutine(Vector3 start, Vector3 end, float duration)
    {
        float currentTime = 0f;
        while(currentTime <= duration)
        {
            transform.position = Vector3.Lerp(start, end, currentTime / duration);

            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}
