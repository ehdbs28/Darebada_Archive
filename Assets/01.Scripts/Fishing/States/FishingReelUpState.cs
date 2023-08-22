using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingReelUpState : FishingState
{
    [SerializeField]
    private Transform _endPos;
    private Vector3 _startPos;

    private Transform _bobberTrm;

    private float _reelUpOffset;
    private int _pointCnt;
    private Vector3 _direction;
    
    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);
        _bobberTrm = agentRoot.Find("Bobber");
    }

    public override void EnterState()
    {
        GameManager.Instance.GetManager<UIManager>().ShowPanel(UGUIType.FishingMiniGame, true, false, false);

        _startPos = _bobberTrm.position;
        Vector3 endPos = _endPos.position;
        endPos.y = 0f;

        _direction = (endPos - _startPos).normalized;
        _pointCnt = GameManager.Instance.GetManager<MiniGameManager>().PointCnt;
        _reelUpOffset = Vector3.Distance(_startPos, endPos) / GameManager.Instance.GetManager<MiniGameManager>().PointCnt;
    }

    public override void ExitState()
    {
        GameManager.Instance.GetManager<UIManager>().GetPanel(UGUIType.FishingMiniGame).RemoveRoot();
    }
    
    public override void UpdateState()
    {
        // 이거 조건 나중에 고치기
        if(Input.GetKeyDown(KeyCode.Space)){
            if (GameManager.Instance.GetManager<MiniGameManager>().Check())
            {
                OnAnswer();
            }
            else
            {
                OnFail();
            }
        }

        base.UpdateState();
    }

    private void OnAnswer(){
        --_pointCnt;
        _bobberTrm.position += _direction * _reelUpOffset;
        _controller.Bait.CatchedFish.transform.position += _direction * _reelUpOffset;
        
        GameManager.Instance.GetManager<MiniGameManager>().Resetting();

        if(_pointCnt <= 0){
            Debug.Log("낚시 성공");
            PoolableParticle particle = GameManager.Instance.GetManager<PoolManager>().Pop("WaterSplashParticle") as PoolableParticle;
            particle.SetPositionAndRotation(_bobberTrm.position, Quaternion.identity);
            particle.Play();
            
            _controller.ActionData.IsFishing = false;
            _controller.ActionData.IsUnderWater = false;

            if (_controller.Bait.CatchedFish != null)
            {
                FishDataUnit dataUnit = _controller.Bait.CatchedFish.DataUnit;
                Vector2 size = new Vector2(dataUnit.InvenSizeX, dataUnit.InvenSizeY);

                GameManager.Instance.GetManager<InventoryManager>().AddUnit(dataUnit, size);
                GameManager.Instance.GetManager<PoolManager>().Push(_controller.Bait.CatchedFish);
                _controller.Bait.CatchedFish = null;
            }
        }
    }

    private void OnFail()
    {
        _controller.Bait.StartCheck = false;
        _controller.Bait.CatchedFish.GetoutBait();
        _controller.Bait.CatchedFish = null;
        
        _controller.ActionData.IsFishing = false;
        _controller.ActionData.IsUnderWater = false;
    }
}