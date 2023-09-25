using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class CutSceneManager : MonoBehaviour
{
    public List<Sprite> cutSceneImages;
    public Image cutSceneImage;
    public bool isScrolled;
    [SerializeField] int _currentImage;
    [SerializeField] bool _onCor;
    [SerializeField] private InputManager _inputManager;
    
    public enum ProgressState
    {
        START,
        READY,
        TOUCHABLE
    }
    public ProgressState progressState = ProgressState.READY;

    private void Start()
    {
        _inputManager.OnTouchEvent += Touch;
        if (PlayerPrefs.GetInt("End", 0) == 1) SetStart();
    }

    IEnumerator FadeOut(float time)
    {
        progressState = ProgressState.READY;
        _onCor = true;
        float a = 1/time * Time.deltaTime;
        Color cor = new Color(0,0,0,a);
        while(true)
        {
            cutSceneImage.color -= cor;
            if (cutSceneImage.color.a <= 0f) break;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        _onCor = false;
        progressState = ProgressState.TOUCHABLE;
    }
    
    IEnumerator FadeIn(float time)
    {
        progressState = ProgressState.READY;
        _onCor = true;
        float a = 1/time * Time.deltaTime;
        Color cor = new Color(0,0,0,a);
        while(true)
        {
            cutSceneImage.color += cor;
            if (cutSceneImage.color.a >= 1f) break;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        _onCor = false;
        progressState = ProgressState.TOUCHABLE;
    }
    
    IEnumerator ScrollUp(float time)
    {
        progressState = ProgressState.READY;
        _onCor = true;
        float a = 1924/time * Time.deltaTime;
        while(true)
        {
            if (GetComponent<RectTransform>().anchoredPosition.y >= 1924f) break;
            GetComponent<RectTransform>().anchoredPosition += Vector2.up * a;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        _onCor = false;
        progressState = ProgressState.TOUCHABLE;
    }
    
    public IEnumerator ShowNext()
    {
        if (_currentImage == cutSceneImages.Count - 1)
        {
            yield return StartCoroutine(FadeOut(1));
            SetStart();
        }
        else
        {
            if(_currentImage != cutSceneImages.Count-2)
            {
                yield return StartCoroutine(FadeOut(1));
                _currentImage++;
                cutSceneImage.sprite = cutSceneImages[_currentImage];
                StartCoroutine(FadeIn(1));
            }else if(!isScrolled)
            {
                StartCoroutine(ScrollUp(1));
                isScrolled= true;
            }
            else
            {
                yield return StartCoroutine(FadeOut(1));
                _currentImage++;
                cutSceneImage.sprite = cutSceneImages[_currentImage];
                StartCoroutine(FadeIn(1));
            }
        }
    }

    private void Touch()
    {
        if (progressState == ProgressState.TOUCHABLE)
        {
            StartCoroutine(ShowNext());
        }
    }
    
    public void SetStart()
    {
        PlayerPrefs.SetInt("End", 1);
        _inputManager.OnTouchEvent -= Touch;
        SceneManager.LoadScene("MainScene 1");
    }

}
