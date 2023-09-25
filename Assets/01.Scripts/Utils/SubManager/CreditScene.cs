using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreditScene : MonoBehaviour
{
    [SerializeField]
    private RectTransform _creditRtrm;

    [SerializeField] 
    private float _creditTime;

    private Coroutine _runningRoutine = null;

    public GameSceneType prevScene;

    public void StartCredit()
    {
        GameManager.Instance.GetManager<UIManager>().Document.rootVisualElement.Q("main-container").visible = false;
        GameManager.Instance.GetManager<InputManager>().OnTouchEvent += OnSkip;
        
        _creditRtrm.anchoredPosition = new Vector2(0, -2760);
        _runningRoutine = StartCoroutine(CreditRoutine());
    }

    private void Exit()
    {
        if (_runningRoutine != null)
        {
            StopCoroutine(_runningRoutine);
            _runningRoutine = null;
        }
        GameManager.Instance.GetManager<UIManager>().Document.rootVisualElement.Q("main-container").visible = true;
        GameManager.Instance.GetManager<InputManager>().OnTouchEvent -= OnSkip;
        GameManager.Instance.GetManager<GameSceneManager>().ChangeScene(prevScene);
    }

    private IEnumerator CreditRoutine()
    {
        float currentTime = 0f;
        float percent = 0f;

        while (percent <= 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / _creditTime;

            float posy = Mathf.Lerp(-2760, 2760, percent);
            _creditRtrm.anchoredPosition = new Vector2(0, posy);

            yield return null;
        }

        Exit();
        _runningRoutine = null;
    }

    private void OnSkip()
    {
        Exit();
    }
}
