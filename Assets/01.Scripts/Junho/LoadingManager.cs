using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    private static LoadingManager _instance;
    public static LoadingManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(LoadingManager)) as LoadingManager;
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<LoadingManager>();
                }
            }

            return _instance;
        }
    }
    public float _loadingSceneStayTime;
    public float _rotSpeed;
    public int _delayPer;

    public bool _isLoading = false;
    public bool _isStart = false;

    private Image _loadingImage;
    private TextMeshProUGUI _loadingTxt;
    
    private GameSceneType _loadingEndGoScene;
    public GameSceneType LoadingEndGoScene
    {
        get { return _loadingEndGoScene; }
        set { _loadingEndGoScene = value; }
    }

    private float _delay;
    private float time;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += Init;
        if (!_isStart)
        {
            _loadingEndGoScene = GameSceneType.Camp;
        }

    }
    
    private void Update()
    {
        print(_loadingSceneStayTime);
        if (_isLoading)
        {
            time += Time.deltaTime;
            if (time > _loadingSceneStayTime)
            {
                SceneManager.LoadScene(0);
                _isLoading = false;
            }
        }
    }

    private void Init(Scene scene, LoadSceneMode mode)
    {
        if (_isLoading)
        {
            _loadingImage = GameObject.Find("LoadingImage").GetComponent<Image>();
            _loadingTxt = FindObjectOfType<TextMeshProUGUI>();

            StartCoroutine(LoadingImgCo());
            StartCoroutine(LoadingTxtCo());
            time = 0;
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