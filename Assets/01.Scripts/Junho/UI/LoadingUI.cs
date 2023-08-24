using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : PoolableMono
{
    public float _rotSpeed;
    public int _delayPer;

    private float _loadingSceneStayTime;
    private float _delay;
    private float time;

    private Image _loadingImage;
    private TextMeshProUGUI _loadingTxt;

    private void Awake()
    {
        _loadingImage = GameObject.Find("LoadingImage").GetComponent<Image>();
        _loadingTxt = FindObjectOfType<TextMeshProUGUI>();
    }


    private void OnEnable()
    {
        _loadingSceneStayTime = Random.Range(5f, 20f);
        LoadingManager.instance.SetLoading(true);

        StartCoroutine(LoadingImgCo());
        StartCoroutine(LoadingTxtCo());
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > _loadingSceneStayTime)
        {
            SceneManager.LoadScene(0);
        }
    }

    public override void Init(){}

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
