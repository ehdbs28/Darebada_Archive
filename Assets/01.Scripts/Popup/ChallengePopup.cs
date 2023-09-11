using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChallengePopup : UIPopup
{
    private List<ChallengeUnit> _challengeList;

    private VisualElement _exitBtn;
    private VisualElement _unitParent;

    [SerializeField]
    private VisualTreeAsset _unitTemplate;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.Instance.GetManager<ChallengeManager>().Renewal(ChallengeType.Revenue, 100);
            Debug.Log(GameManager.Instance.GetManager<ChallengeManager>().TotalRevenue);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            GameManager.Instance.GetManager<ChallengeManager>().Renewal(ChallengeType.AmountSpent, 100);
            Debug.Log(GameManager.Instance.GetManager<ChallengeManager>().TotalAmountSpent);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.GetManager<ChallengeManager>().Renewal(ChallengeType.FishCount, 1);
            Debug.Log(GameManager.Instance.GetManager<ChallengeManager>().TotalFishcount);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            DictionaryData data = GameManager.Instance.GetManager<DataManager>().GetData(DataType.DictionaryData) as DictionaryData;
            DictionaryDataUnit dataUnit = new DictionaryDataUnit();
            dataUnit.Name = "Dolphin";
            data.Units.List.Add(dataUnit);
            Debug.Log("Dolphin");
            GameManager.Instance.GetManager<ChallengeManager>().Renewal(ChallengeType.FishCount, 1);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            DictionaryData data = GameManager.Instance.GetManager<DataManager>().GetData(DataType.DictionaryData) as DictionaryData;
            DictionaryDataUnit dataUnit = new DictionaryDataUnit();
            dataUnit.Name = "RainbowFish";
            data.Units.List.Add(dataUnit);
            Debug.Log("RainbowFish");
            GameManager.Instance.GetManager<ChallengeManager>().Renewal(ChallengeType.FishCount, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            DictionaryData data = GameManager.Instance.GetManager<DataManager>().GetData(DataType.DictionaryData) as DictionaryData;
            DictionaryDataUnit dataUnit = new DictionaryDataUnit();
            dataUnit.Name = "StingRay";
            data.Units.List.Add(dataUnit);
            Debug.Log("StingRay");
            GameManager.Instance.GetManager<ChallengeManager>().Renewal(ChallengeType.FishCount, 1);
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            DictionaryData data = GameManager.Instance.GetManager<DataManager>().GetData(DataType.DictionaryData) as DictionaryData;
            DictionaryDataUnit dataUnit = new DictionaryDataUnit();
            dataUnit.Name = "SunFish";
            data.Units.List.Add(dataUnit);
            Debug.Log("SunFish");
            GameManager.Instance.GetManager<ChallengeManager>().Renewal(ChallengeType.FishCount, 1);
        }
    }

    public override void SetUp(UIDocument document, bool clearScreen = true, bool blur = true, bool timeStop = true)
    {
        base.SetUp(document, clearScreen, blur, timeStop);

        ChallengeDataTable dataUnits = (ChallengeDataTable)GameManager.Instance.GetManager<SheetDataManager>().GetData(DataLoadType.ChallengeData);

        _challengeList = GameManager.Instance.GetManager<ChallengeManager>().Challenges;
        for (int i = 0; i < _challengeList.Count; i++)
        {
            Debug.Log(i);
            VisualElement root = _unitTemplate.Instantiate();
            root = root.Q<VisualElement>("template-container");
            _challengeList[i].Generate(root, dataUnits.DataTable[i], i);
            _unitParent.Add(root);
        }
    }

    public override void AddEvent()
    {
        _exitBtn.RegisterCallback<ClickEvent>(e =>
        {
            RemoveRoot();
        });
    }

    public override void FindElement()
    {
        _exitBtn = _root.Q<VisualElement>("exit-btn");
        _unitParent = _root.Q<ScrollView>("challenge-list");
    }

    public override void RemoveEvent()
    {

    }
}
