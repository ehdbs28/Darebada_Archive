using Core;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquariumNumericalManager : MonoBehaviour,IManager
{
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
    public void InitManager()
    {
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
        EntrancePercent = Mathf.Clamp((float)((float)fishbowlCnt / (float)EntranceFee) * 100f, 10f, 200f);
        Reputation = Mathf.Clamp((EntrancePercent / 100f * (100-CleanScore )/ 100f * ArtScore / 100f) * 100f + PromotionPoint, 10, 100);
        ArtScore = Mathf.Clamp(((float)(decoCnt / 2) / decoCnt) * 100, 10, 100);
    }

    public void OnDayChange(GameDate dateTime)
    {
        CleanScore = (int)Mathf.Clamp(CleanScore - Reputation * 3, 0, 100);
        int dispointAmount = _promotionPoint;
        if (PromotionPoint > 0) PromotionPoint -= dispointAmount;
        GameManager.Instance.GetManager<MoneyManager>().AddMoney(CustomerCnt * EntranceFee + shopRevenue - employeeCnt * 100 - cleanServiceAmount - shopRevenue / 10 * 2);
    }
}
