using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Core;
using UnityEngine;

public class CampSubManager : MonoBehaviour, IManager
{
    [SerializeField]
    private MeshFilter _boatMeshFilter;
    
    [SerializeField]
    private MeshRenderer _boatMeshRenderer;

    [SerializeField] 
    private float _titleDelayOffset = 1.5f;

    private TutorialManager _tutorial;
    private bool _touch;
    
    public void EnterSceneEvent()
    {
        GameManager.Instance.Managers.Add(this);
        BoatDataUnit boatDataUnit = GameManager.Instance.GetManager<BoatManager>().CurrentBoatData;
        SetBoatVisual(boatDataUnit);
        ShowTitle();
    }

    public void ExitSceneEvent()
    {
        GameManager.Instance.Managers.Remove(this);
    }

    public void SetBoatVisual(BoatDataUnit dataUnit)
    {
        _boatMeshFilter.mesh = dataUnit.Visual.VisualMesh;
        _boatMeshRenderer.material = dataUnit.Visual.MainMat;
    }

    private void ShowTitle()
    {
        CinemachineBlenderSettings blenderSettings = Define.MainCam.GetComponent<CinemachineBrain>().m_CustomBlends;
        float duration = blenderSettings.m_CustomBlends[^1].m_Blend.BlendTime;
        StartCoroutine(TitleRoutine(duration));
    }

    private IEnumerator TitleRoutine(float duration)
    {
        TitleScreen titleScreen = (TitleScreen)GameManager.Instance.GetManager<UIManager>().GetPanel(ScreenType.Title);
        
        float curTime = 0f;
        float percent = 0f;

        while (percent <= 1f)
        {
            curTime += Time.deltaTime;
            percent = curTime / _titleDelayOffset;
            titleScreen.SetAlpha(percent);
            yield return null;
        }

        GameManager.Instance.GetManager<InputManager>().OnTouchEvent += OnTitleTouchEvent;
        yield return new WaitUntil((() => _touch));
        GameManager.Instance.GetManager<InputManager>().OnTouchEvent -= OnTitleTouchEvent;
        titleScreen.TapToStart.visible = false;
        
        
        GameManager.Instance.GetManager<CameraManager>().SetVCam(CameraState.CAMP);
        
        curTime = duration - _titleDelayOffset;
        percent = 0f;
        
        while (percent >= 0f)
        {
            curTime -= Time.deltaTime;
            percent = curTime / (duration - _titleDelayOffset);
            titleScreen.SetAlpha(percent);
            yield return null;
        }

        yield return new WaitForSeconds(_titleDelayOffset);
        
        GameManager.Instance.GetManager<UIManager>().ShowPanel(ScreenType.Camp);

        _tutorial = FindObjectOfType<TutorialManager>().GetComponent<TutorialManager>();
        _tutorial.OnClickEvent();
    }

    private void OnTitleTouchEvent()
    {
        _touch = true;
    }

    public void ResetManager()
    {
    }

    public void InitManager()
    {
    }

    public void UpdateManager()
    {
    }
}
