using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    public float _rotSpeed;
    public int _delayPer;

    private float _loadingSceneStayTime;
    private float _delay;
    private float time;

    [SerializeField] private Image _loadingImage;
    [SerializeField] private TextMeshProUGUI _loadingTxt;


    private void OnEnable()
    {   
        _loadingSceneStayTime = Random.Range(2f, 2.8f);
        time = 0;
        print("stayTime : " + _loadingSceneStayTime);
        if (GameManager.Instance.GetManager<LoadingManager>().IsStart)
        {
            GameManager.Instance.GetManager<LoadingManager>().IsLoading = true;
        }

        StartCoroutine(LoadingImgCo());
        StartCoroutine(LoadingTxtCo());
    }

    private void Update()
    {
        time += Time.deltaTime; 
        if (time > _loadingSceneStayTime)
        {
            GameManager.Instance.GetManager<GameSceneManager>().ChangeScene(GameManager.Instance.GetManager<LoadingManager>().next);
            GameManager.Instance.GetManager<LoadingManager>().IsLoading = false;
        }
    }

    IEnumerator LoadingImgCo()
    {
        while (true)
        {
            int rand = Random.Range(0, 100);
            if (rand > _delayPer)
            {
                _loadingImage.transform.Rotate(0, 0, -_rotSpeed);
            }
            else
            {
                _delay = Random.Range(0, 1);
                yield return new WaitForSeconds(_delay);
            }
        }
    }
    IEnumerator LoadingTxtCo()
    {
        while (true)
        {
            int rand = Random.Range(0, 100);
            if (rand > _delayPer)
            {
                _loadingTxt.text = "Loading";
                for (int i = 0; i < 3; i++)
                {
                    _loadingTxt.text += ".";
                    yield return new WaitForSeconds(0.3f);
                }
            }
            else
            {
                _delay = Random.Range(0, 1);
                yield return new WaitForSeconds(_delay);
            }
        }
    }

}
