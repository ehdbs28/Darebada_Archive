using Core;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquariumNumericalManager : MonoBehaviour,IManager
{
    [SerializeField] private GameObject _aquariumInitObj;
    public GameObject AquariumInitObj => _aquariumInitObj;
    
    public Material fishBowlMat;
    public Material mossMat;
    [SerializeField] Color pureWaterColor;
    [SerializeField] Color corruptedWaterColor;

    [SerializeField] private int _cleanScore;
    public int CleanScore
    {
        get { return _cleanScore; }
        set
        {
            _cleanScore = value;
            if (_cleanScore < 50)
            {
                fishBowlMat.SetColor("Color_77A2EDE9", pureWaterColor);
            }
            else
            {
                fishBowlMat.SetColor("Color_77A2EDE9", corruptedWaterColor);
            }
            mossMat.SetFloat("_ShowValue", 1f - CleanScore / 100f + 0.3f);
        }
    }

    [SerializeField] private int _promotionPoint;
    public int PromotionPoint
    {
        get { return _promotionPoint; }
        set { _promotionPoint = value; }
    }
    public int EntranceFee
    {
        get
        {
            int star = (int)_reputation / 20;
            return star * 15;
        }
    }
    [SerializeField] private float _entrancePercent;
    public float EntrancePercent
    {
        get { return _entrancePercent; }
        set { _entrancePercent = value; }
    }
    [SerializeField] private float _reputation;
    public float Reputation
    {
        get { return _reputation; }
        set { _reputation = value; }
    }
    [SerializeField] private float _artScore;
    public float ArtScore
    {
        get { return _artScore; }
        set { _artScore = value; }
    }

    public int CustomerCnt
    {
        get
        {
            int star = (int)_reputation / 20;
            return star * 10;
        }    
    }
    
    public int decoCnt = 0;
    public int fishbowlCnt = 0;
    public int shopCnt = 0;
    public int roadCnt = 0;

    public int shopRevenue;
    public int employeeCnt;
    public int cleanServiceAmount;

    public List<GameObject> fishBowls = new List<GameObject>();
    public List<DecoVisualSO> decoVisuals = new List<DecoVisualSO>();

    [SerializeField] int _promoDispointAmount;

    public event Action<float, float, float, float> OnReputationChanged;
    public void InitManager()
    {
        GameManager.Instance.GetManager<TimeManager>().OnTimeChangedEvent += (int a, int b, float c) =>
        {
            AquariumData data = GameManager.Instance.GetManager<DataManager>().GetData(DataType.AquariumData) as AquariumData;
            data.CleanScore = CleanScore;
            data.CleanService_Amount = cleanServiceAmount;
            data.Decoration_Count = decoCnt;
            data.Fishbowl_Count = fishbowlCnt;
            data.PromotionPoint = PromotionPoint;
            data.Road_Count = roadCnt;
            data.Shop_Count = shopCnt;
        };
        ResetManager();
    }
    [SerializeField] private Vector3 _floorSize = new Vector3(1, 1, 1);
    public Vector3 FloorSize
    {
        get { return _floorSize; }
        set { _floorSize = value; }
    }

    public void ResetManager()
    {
        GameManager.Instance.GetManager<TimeManager>().OnDayChangedEvent += OnDayChange;
    }

    public void UpdateManager()
    {

        GameManager.Instance.GetManager<DataManager>().SaveDataAll();
        CleanScore = Mathf.Clamp(_cleanScore, 0, 100);
    
        if (EntranceFee == 0)
            EntrancePercent = 0;
        else
            EntrancePercent = Mathf.Clamp((float)((float)fishbowlCnt / (float)EntranceFee) * 100f, 0f, 200f);

        if (fishbowlCnt == 0)
            ArtScore = 0;
        else
            ArtScore = Mathf.Clamp(decoCnt / 2f / (float)fishbowlCnt * 100f, 0f, 100f);
    
        Reputation = Mathf.Clamp(((100 - CleanScore) / 100f) * (ArtScore / 100f) * 100f + PromotionPoint, 0, 100);
        OnReputationChanged?.Invoke(EntrancePercent, _cleanScore, _artScore, _reputation);
    }

    public void OnDayChange(GameDate dateTime)
    {
        CleanScore = (int)Mathf.Clamp(CleanScore - Reputation * 3, 0, 100);
        int dispointAmount = _promotionPoint;
        if (PromotionPoint > 0) PromotionPoint -= dispointAmount;
        GameManager.Instance.GetManager<MoneyManager>().AddMoney(CustomerCnt * EntranceFee + shopRevenue - employeeCnt * 100 - cleanServiceAmount - shopRevenue / 10 * 2);
    }
}
