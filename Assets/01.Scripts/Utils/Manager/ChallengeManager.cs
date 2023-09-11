using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

public class ChallengeManager : IManager
{
    private List<ChallengeUnit> _challenges;
    public List<ChallengeUnit> Challenges => _challenges;

    private int _totalRevenue;
    public int TotalRevenue => _totalRevenue;

    private int _totalAmountSpent;
    public int TotalAmountSpent => _totalAmountSpent;

    private int _totalFishCount;
    public int TotalFishcount => _totalFishCount;

    private ChallengeData _data;

    public void Renewal(ChallengeType type, int addValue)
    {
        switch (type)
        {
            case ChallengeType.Revenue:
                _totalRevenue += addValue;
                break;
            case ChallengeType.AmountSpent:
                _totalAmountSpent += addValue;
                break;
            case ChallengeType.FishCount:
                _totalFishCount += addValue;
                break;
            default:
                break;
        }

        for(int i = 0; i < _challenges.Count; i++)
        {
            if (!_data.units[i].isClear)
            {
                int curState = 0;
                switch (_challenges[i].Type)
                {
                    case ChallengeType.Revenue:
                        curState = _totalRevenue;
                        break;
                    case ChallengeType.AmountSpent:
                        curState = _totalAmountSpent;
                        break;
                    case ChallengeType.FishCount:
                        if(_challenges[i].TargetFishName == "")
                        {
                            curState = _totalFishCount;
                        }
                        else
                        {
                            DictionaryData data = GameManager.Instance.GetManager<DataManager>().GetData(DataType.DictionaryData) as DictionaryData;
                            DictionaryDataUnit dataUnit = data.Units.List.Find(unit => unit.Name == _challenges[i].TargetFishName);

                            if(dataUnit == null)
                            {
                                curState = 0;
                            }
                            else
                            {
                                Debug.Log(1);
                                curState = 1;
                            }
                        }
                        break;
                    default:
                        break;
                }
                _challenges[i].CheckConditions(curState) ;

            }
        };
    }

    public void InitManager()
    {
        _challenges = new List<ChallengeUnit>();
        int size = ((ChallengeDataTable)GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.ChallengeData)).Size;
        for(int i = 0; i < size; ++i)
        {
            _challenges.Add(new ChallengeUnit());
        }
        _data = (ChallengeData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.ChallengeData);
    }

    public void ResetManager()
    {

    }

    public void UpdateManager()
    {
        
    }
}
