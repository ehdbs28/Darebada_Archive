using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentHealth : MonoBehaviour
{
    [SerializeField] private float _maxHp;

    private float _currentHp = 0f;
    public float CurrentHp => _currentHp;

    public UnityEvent OnPlayerDieEvent = null;

    private void Awake() {
        ResetHp();
    }

    public void ResetHp(){
        _currentHp = _maxHp;
    }

    public void DecreaseHp(float value){
        _currentHp -= value;

        if(_currentHp <= 0)
            OnPlayerDieEvent?.Invoke();
    }
}
