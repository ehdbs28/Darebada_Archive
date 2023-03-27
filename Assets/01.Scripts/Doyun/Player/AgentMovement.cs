using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.5f;

    private AgentInput _agentInput;

    private void Awake()
    {
        _agentInput = GetComponent<AgentInput>();
    }

    private void Start()
    {
        _agentInput.OnMouseClickEvent += OnMove;
    }

    private void OnMove(Vector3 mousePos)
    {
        DOMove(transform.position, mousePos, _moveSpeed);
    }

    private void OnRotate(Vector3 mousePos)
    {

    }

    private void DOMove(Vector3 start, Vector3 end, float duration)
    {
        StartCoroutine(DOMoveCoroutine(start, end, duration));
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
