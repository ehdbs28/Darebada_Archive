using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

public class ChallengeManager : IManager
{
    private List<ChallengeDataUnit> _challenges;
    public List<ChallengeDataUnit> Challenges => _challenges;

    private int _totalRevenue;
    public int TotalRevenue => _totalRevenue;

    private int _totalAmountSpent;
    public int TotalAmountSpent => _totalAmountSpent;

    private int _totalFishCount;
    public int TotalFishcount => _totalFishCount;

    private ChallengeData _data;
    public BiomeData BiomeData;

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
                switch (_challenges[i].Type)
                {
                    case ChallengeType.Revenue:
                        _data.units[i].totalValue = _totalRevenue;
                        break;
                    case ChallengeType.AmountSpent:
                        _data.units[i].totalValue = _totalAmountSpent;
                        break;
                    case ChallengeType.FishCount:
                        if(_challenges[i].TargetFishName == "")
                        {
                            _data.units[i].totalValue = _totalFishCount;
                            Debug.Log(_challenges[i].TargetFishName);
                            Debug.Log($"일반 퀘스트 {i}: {_data.units[i].totalValue}");
                        }
                        else
                        {
                            DictionaryData data = GameManager.Instance.GetManager<DataManager>().GetData(DataType.DictionaryData) as DictionaryData;
                            DictionaryDataUnit dataUnit = data.Units.List.Find(unit => unit.Name == _challenges[i].TargetFishName);
                            int num = 0;
                            data.Units.List.ForEach(unit =>
                            {
                                Debug.Log($"{num++}번째 TargetFishName: {unit.Name}");
                                Debug.Log(unit.Name == _challenges[i].TargetFishName);
                            });

                            Debug.Log($"TEST{i}: {dataUnit == null}");

                            if(dataUnit == null)
                            {
                                _data.units[i].totalValue = 0;
                                Debug.Log($"못 잡음 {i}: {_data.units[i].totalValue}");
                            }
                            else
                            {
                                _data.units[i].totalValue = 1;
                                Debug.Log($"이미 잡음 {i}: {_data.units[i].totalValue}");
                            }
                        }
                        break;
                    default:
                        break;
                }
                CheckConditions(_data.units[i].totalValue, _challenges[i].Target, i);
            }
        };
    }
    
    public void CheckConditions(int curState, int target, int idx)
    {
        if (curState >= target)
        {
            _data.units[idx].isClear = true;
            Debug.Log($"TEST{idx}: {!(_challenges[idx].TargetFishName == "")}");
            if(!string.IsNullOrEmpty(_challenges[idx].TargetFishName))
                BiomeData.Biomes[idx + 1] = true;
        }
    }

    public void InitManager()
    {
        _challenges = new List<ChallengeDataUnit>();

        var sheetData = (ChallengeDataTable)GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.ChallengeData);
        for(int i = 0; i < sheetData.Size; ++i)
        {
            _challenges.Add(sheetData.DataTable[i]);
        }
        _data = (ChallengeData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.ChallengeData);
        BiomeData = (BiomeData)GameManager.Instance.GetManager<DataManager>().GetData(DataType.BiomeData);
    }

    public void ResetManager()
    {

    }

    public void UpdateManager()
    {
        
    }
}
