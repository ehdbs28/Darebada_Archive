using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingCatchingState : FishingState
{
    private Transform _bobberTrm;

    private bool _isReadyToCatch;

    private Vector3 _start;
    private Vector3 _end;

    private float _throwTime;
    private float _currentTime = 0f;
    private float _percent = 0f;

    private LayerMask _fishLayer;

    private Fish _currentCatchFish = null;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);

        _bobberTrm = agentRoot.Find("Bobber");
        _fishLayer = LayerMask.GetMask("Fish");
    }

    public override void EnterState()
    {
        _start = _bobberTrm.position;
        _end = _start + _controller.ActionData.LastThrowDirection.normalized; 

        float stringLength = 10 - _controller.ActionData.LastChargingPower * 10 / _controller.FishingData.MaxChargingPower + 1; 
        // 10 이라는 상수 값은 후에 최대 낚시 찌가 들어갈 거리로 바꿔줘야 함
        
        _end.y = -stringLength;

        _throwTime = Mathf.Max(0.3f, Vector3.Distance(_start, _end)) / _controller.FishingData.ThrowingSpeed;
        _currentTime = 0f;
        _percent = 0f;

        _isReadyToCatch = false;

        StartCoroutine(ToThrow());
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        if(_isReadyToCatch){
            // 여기서 물고기 끌고오고 미니게임 들어가야 함
            if(_currentCatchFish == null){
                //Collider[]_aroundFish = Physics.OverlapSphere(_bobberTrm.position, 5f, _fishLayer);

                // 나중에 조건 고치기
                if(Input.GetKey(KeyCode.Space)){
                    _percent -= _controller.FishingData.ThrowingSpeed * Time.deltaTime / _throwTime;
                    _bobberTrm.position = GetLerpPos();

                    if(_percent <= 0){
                        _controller.ActionData.IsFishing = false;
                        _controller.ActionData.IsUnderWater = false;
                    }
                }
            }
            else{

            }
        }
        else{
            // 나중에 조건 고치기
            if(Input.GetKeyDown(KeyCode.Space)){
                _percent = 1f;
                _isReadyToCatch = true;
            }
        }

        base.UpdateState();
    }

    private IEnumerator ToThrow(){
        while(_percent < 1 && _isReadyToCatch == false){
            _currentTime += _controller.FishingData.ThrowingSpeed * Time.deltaTime;
            _percent = _currentTime / _throwTime;

            _bobberTrm.position = GetLerpPos();

            yield return null;
        }

        _percent = 1f;
        _bobberTrm.position = GetLerpPos();

        _isReadyToCatch = true;

        _start = new Vector3(_controller.ActionData.InitPosition.x, 0, _controller.ActionData.InitPosition.z);
    }

    private Vector3 GetLerpPos(){
        return Vector3.Lerp(_start, _end, _percent);
    }
}
