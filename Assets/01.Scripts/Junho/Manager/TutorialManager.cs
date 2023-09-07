using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour, IManager
{
    [SerializeField]Image _tutorialImage;
    [SerializeField]Image _tutorialPanel;

    [SerializeField]List<Sprite> _campExplainImages;
    [SerializeField]List<Sprite> _oceanExplainImages;
    [SerializeField]List<Sprite> _aquariumExplainImages;

    private int idx = 0;
    private GameSceneType _sceneType;

    public void ResetManager(){}
    public void InitManager()
    {
        ShowTutorial(false);
    }
    public void UpdateManager(){}

    public void OnClickEvent(GameSceneType sceneType)
    {
        print("startTutorial");
        idx = 0;
        _sceneType = sceneType;
        // 클릭, 터치
        GameManager.Instance.GetManager<InputManager>().OnTouchEvent += StartTutorial;
        StartTutorial();
    }

    private void ShowTutorial(bool value)
    {
        _tutorialImage.gameObject.SetActive(value);
        _tutorialPanel.gameObject.SetActive(value);
    }

    private void StartTutorial()
    {
        GameData gameData = GameManager.Instance.GetManager<DataManager>().GetData(DataType.GameData) as GameData;
        
        print(_sceneType);
        // 이거 나중에 유지보수 때 바꿔야 함 일단 기능만 구현함
        if (_sceneType == GameSceneType.Camp)
        {
            if (idx == _campExplainImages.Count) EndTutorial(_sceneType);
            if (gameData.CampTutorial) return;
        }
        if (_sceneType == GameSceneType.Ocean)
        {
            if (idx == _oceanExplainImages.Count) EndTutorial(_sceneType);
            if (gameData.OceanTutorial) return;
        }
        if (_sceneType == GameSceneType.Aquarium)
        {
            if (idx == _campExplainImages.Count) EndTutorial(_sceneType);
            if (gameData.CampTutorial) return;
        }


        ShowTutorial(true);

        if (_sceneType == GameSceneType.Camp) _tutorialImage.sprite = _campExplainImages[idx++];
        if (_sceneType == GameSceneType.Ocean) _tutorialImage.sprite = _oceanExplainImages[idx++];
        if (_sceneType == GameSceneType.Aquarium) _tutorialImage.sprite = _aquariumExplainImages[idx++];
    }

    private void EndTutorial(GameSceneType sceneType)
    {
        print("TutorialEnd");
        GameData gameData = GameManager.Instance.GetManager<DataManager>().GetData(DataType.GameData) as GameData;
        GameManager.Instance.GetManager<InputManager>().OnTouchEvent -= StartTutorial;
        ShowTutorial(false);

        if (sceneType == GameSceneType.Camp)
            gameData.CampTutorial = true;
        else if (sceneType == GameSceneType.Ocean)
            gameData.OceanTutorial = true;
        else if (sceneType == GameSceneType.Aquarium)
            gameData.AquariumTutorial = true;
    }



}
