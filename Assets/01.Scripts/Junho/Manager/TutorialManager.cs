using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]Image _tutorialImage;
    [SerializeField]Image _tutorialPanel;
    [SerializeField]List<Sprite> _explainImages;

    private int idx = 0;

    private void Awake()
    {
        ShowTutorial(false);
    }

    public void OnClickEvent()
    {
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
        if (idx == _explainImages.Count) EndTutorial();
        if (gameData.Tutorial) return;

        ShowTutorial(true);
        _tutorialImage.sprite = _explainImages[idx++];
    }

    private void EndTutorial()
    {
        GameData gameData = GameManager.Instance.GetManager<DataManager>().GetData(DataType.GameData) as GameData;
        gameData.Tutorial = true;
        ShowTutorial(false);
    }

}
