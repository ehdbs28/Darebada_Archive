using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    
    [SerializeField]
    private float _jumpUpHeight;

    [SerializeField]
    private float _rotateSpeedX;
    
    [SerializeField]
    private float _rotateSpeedZ;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);
        _bobberTrm = agentRoot.Find("Bobber");
    }

    public override void EnterState()
    {
        GameManager.Instance.GetManager<InputManager>().OnTouchEvent += OnTouch;
        GameManager.Instance.GetManager<UIManager>().ShowPanel(UGUIType.FishingMiniGame, true);

        _startPos = _bobberTrm.position;
        Vector3 endPos = _endPos.position;
        endPos.y = 0f;

        _direction = (endPos - _startPos).normalized;
        _pointCnt = GameManager.Instance.GetManager<MiniGameManager>().PointCnt;
        _reelUpOffset = Vector3.Distance(_startPos, endPos) / GameManager.Instance.GetManager<MiniGameManager>().PointCnt;
    }

    public override void ExitState()
    {
        GameManager.Instance.GetManager<InputManager>().OnTouchEvent -= OnTouch;
        GameManager.Instance.GetManager<UIManager>().GetPanel(UGUIType.FishingMiniGame).RemoveRoot();
    }

    private void OnTouch()
    {
        if (GameManager.Instance.GetManager<MiniGameManager>().Check())
        {
            OnAnswer();
        }
        else
        {
            OnFail();
        }
    }

    private void OnAnswer(){
        --_pointCnt;
        _bobberTrm.position += _direction * _reelUpOffset;
        _controller.Bait.CatchedFish.transform.position += _direction * _reelUpOffset;
        
        GameManager.Instance.GetManager<MiniGameManager>().Resetting();

        if(_pointCnt <= 0){
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
                
                DictionaryData data = (DictionaryData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.DictionaryData);
                DictionaryDataUnit unit = data.Units.List.Find(unit => unit.Name == dataUnit.Name);

                if(unit == null)
                {
                    data.Units.List.Add(new DictionaryDataUnit
                    {
                        Name = dataUnit.Name,
                        Image = dataUnit.Visual.Profile,
                        Desc = dataUnit.Description,
                        Habitat = dataUnit.Habitat,
                        Favorites = dataUnit.Favorites,
                        MaxLenght = _controller.Bait.CatchedFish.ActionData.Lenght,
                        MaxWeight = _controller.Bait.CatchedFish.ActionData.Weight,
                        IsDotated = false
                    });
                }
                else
                {
                    unit.MaxLenght = Mathf.Max(unit.MaxLenght, _controller.Bait.CatchedFish.ActionData.Lenght);
                    unit.MaxWeight = Mathf.Max(unit.MaxWeight, _controller.Bait.CatchedFish.ActionData.Weight);
                }

                _controller.Bait.CatchedFish.SuccessCatching(
                    _controller.transform.position,
                    _jumpUpHeight,
                    _rotateSpeedX,
                    _rotateSpeedZ
                );

                _controller.Bait.CatchedFish = null;

                //((CatchedFishCheckingPopup)GameManager.Instance.GetManager<UIManager>().GetPanel(PopupType.CatchedFishChecking)).dataUnit = dataUnit;
                //GameManager.Instance.GetManager<UIManager>().ShowPanel(PopupType.CatchedFishChecking);

                GameManager.Instance.GetManager<ChallengeManager>().Renewal(ChallengeType.FishCount, 1);
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