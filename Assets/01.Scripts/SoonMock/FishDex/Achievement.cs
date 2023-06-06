using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Achievement : MonoBehaviour
{
    public AchievementSO arcSO;
    public GameObject fishIconObject;
    public Transform parentTrm;

    [SerializeField] private Button _rewardButton;
    [SerializeField] private Image _rewardImage;
    [SerializeField] private TextMeshProUGUI _rewardText;
    public void Init()
    {
        foreach(FishInfoSO fish in arcSO.completedList)
        {

            parentTrm = GetComponentInChildren<GridLayoutGroup>().transform;
            GameObject obj = Instantiate(fishIconObject, parentTrm);
            FishIcon sc = obj.GetComponent<FishIcon>();
            sc.fishInfo = fish;
            sc.Show();
        }
    }
    int _debugCount = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddCollectedFish(arcSO.completedList[_debugCount]);
            _debugCount++;
            _debugCount %= arcSO.completedList.Count;
        }
    }
    private void FixedUpdate()
    {
        CheckComplete();
        _rewardImage.sprite = arcSO.rewardImage;
        _rewardText.text = arcSO.rewardText;
        _rewardButton.enabled = arcSO.isCompleted && !arcSO.isRewardTaked;
    }
    public void AddCollectedFish(FishInfoSO so)
    {
        if (arcSO.completedList.Exists((value) => { return so == value; }) && !arcSO.infoList.Exists((value) => { return so == value; }))
        {
            arcSO.infoList.Add(so);
        }
        CheckComplete();
    }
    public void CheckComplete()
    {
        bool isCompleted = false;
        foreach(FishInfoSO fish in arcSO.completedList)
        {
            foreach(FishInfoSO ff in arcSO.infoList)
            {
                if(ff.fishName == fish.fishName)
                {
                    isCompleted = true;
                    break;
                }else
                {
                    isCompleted = false;    
                }
            }
        }
        arcSO.isCompleted = isCompleted;
    }
    public void TakeReward()
    {
        //MoneyManager.Instance.money += arcSO.rewardAmount;
        arcSO.isRewardTaked = true;
    }
}