using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.XR;

public class CutSceneManager : MonoBehaviour
{
    public List<Sprite> cutSceneImages;
    public Image cutSceneImage;
    public bool isScrolled;
    [SerializeField] int _currentImage;
    [SerializeField] bool _onCor;
 
    public enum ProgressState
    {
        START,
        READY,
        TOUCHABLE
    }
    public ProgressState progressState = ProgressState.READY;
    
    IEnumerator FadeOut(float time)
    {
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
    }
    IEnumerator FadeIn(float time)
    {
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
    }
    IEnumerator ScrollUp(float time)
    {
        _onCor = true;
        float a = 1924/time * Time.deltaTime;
        while(true)
        {
            if (GetComponent<RectTransform>().anchoredPosition.y >= 1924f) break;
            GetComponent<RectTransform>().anchoredPosition += Vector2.up * a;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        _onCor = false;
    }
    public IEnumerator ShowNext()
    {
        Debug.Log("Asdf");
        if(_currentImage != cutSceneImages.Count-1)
        {
            yield return StartCoroutine(FadeOut(1));
            _currentImage++;
            cutSceneImage.sprite = cutSceneImages[_currentImage];
            StartCoroutine(FadeIn(1));
        }else if(!isScrolled)
        {
            StartCoroutine(ScrollUp(1));
            isScrolled= true;
        }else
        {
            SetStart();
        }
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))StartCoroutine(ShowNext());
    }
    public void SetStart()
    {
        //게임 시작할 때 쓸걸 넣어야함
    }

}
