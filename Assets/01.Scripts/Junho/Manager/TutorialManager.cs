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

    private bool _inTut;
    public bool InTut => _inTut;   

    public void ResetManager(){}
    public void InitManager()
    {
        ShowTutorial(false);
    }
    public void UpdateManager(){}

    public void OnTutorial(GameSceneType sceneType)
    {
        idx = 0;
        _sceneType = sceneType;
        _inTut = true;
        GameManager.Instance.GetManager<InputManager>().OnTutorialClick += StartTutorial;
        StartTutorial();
    }

    private void ShowTutorial(bool value)
    {
        if (!value)
        {
            _inTut = false;
            GameManager.Instance.GetManager<InputManager>().OnTutorialClick -= StartTutorial;
        }
        
        _tutorialImage.gameObject.SetActive(value);
        _tutorialPanel.gameObject.SetActive(value);
    }

    private void StartTutorial()
    {
        GameData gameData = GameManager.Instance.GetManager<DataManager>().GetData(DataType.GameData) as GameData;
        
        if (_sceneType == GameSceneType.Camp)
        {
            if (idx == _campExplainImages.Count) EndTutorial(_sceneType);
            if (gameData.CampTutorial)
            {
                _inTut = false;
                return;
            }
        }
        if (_sceneType == GameSceneType.Ocean)
        {
            if (idx == _oceanExplainImages.Count) EndTutorial(_sceneType);
            if (gameData.OceanTutorial)
            {
                _inTut = false;
                return;
            }
        }
        if (_sceneType == GameSceneType.Aquarium)
        {
            if (idx == _campExplainImages.Count) EndTutorial(_sceneType);
            if (gameData.CampTutorial)
            {
                _inTut = false;
                return;
            }
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
        GameManager.Instance.GetManager<InputManager>().OnTutorialClick -= StartTutorial;
        ShowTutorial(false);
        _inTut = false;

        if (sceneType == GameSceneType.Camp)
            gameData.CampTutorial = true;
        else if (sceneType == GameSceneType.Ocean)
            gameData.OceanTutorial = true;
        else if (sceneType == GameSceneType.Aquarium)
            gameData.AquariumTutorial = true;
    }
}
