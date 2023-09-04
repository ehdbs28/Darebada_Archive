using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour, IManager
{
    [SerializeField]
    private AudioMixer _masterMixer;
    
    [SerializeField]
    private AudioMixerGroup _bgmGroup;
    
    [SerializeField]
    private AudioMixerGroup _sfxGroup;
    
    public AudioClip uiClickSoundClip;
    public AudioClip playerThrowingSoundClip;

    public float soundFadeOnTime;

    private AudioSource[] _audioSources = new AudioSource[(int)SoundEnum.SOUNDCOUNT];

    public void InitManager() // Awake
    {
        string[] soundNames = System.Enum.GetNames(typeof(SoundEnum));
        for (int i = 0; i < soundNames.Length - 1; i++)
        {
            GameObject go = new GameObject { name = soundNames[i] };
            _audioSources[i] = go.AddComponent<AudioSource>();
            _audioSources[i].playOnAwake = false;
            _audioSources[i].outputAudioMixerGroup = (soundNames[i] == "BGM" ? _bgmGroup : _sfxGroup);
            go.transform.parent = transform;
        }

        _audioSources[(int)SoundEnum.BGM].loop = true;

    }
    public void ResetManager(){}

    public void UpdateManager(){}

    public void Play(AudioClip audioClips, SoundEnum type = SoundEnum.EFFECT)
    {
        if (audioClips == null)
        {
            Debug.LogError("cannot find audioclips");
            return;
        }

        if (type == SoundEnum.BGM)
        {
            StopAllCoroutines();
            AudioSource audioSource = _audioSources[(int)SoundEnum.BGM];

            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.volume = 0;
            audioSource.clip = audioClips;
            audioSource.Play();

            StartCoroutine(SoundFade(true, _audioSources[(int)SoundEnum.BGM], soundFadeOnTime, 1, SoundEnum.BGM));
            StartCoroutine(SoundFade(false, _audioSources[(int)SoundEnum.BGM], soundFadeOnTime, 0, SoundEnum.BGM));
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)SoundEnum.EFFECT];
            audioSource.PlayOneShot(audioClips); // ����� ��ø ���� �� �Լ�
        }
    }

    public void Stop()
    {
        foreach (var audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
    }

    public void ClickSound()
    {
        Play(uiClickSoundClip, SoundEnum.EFFECT);
    }

    IEnumerator SoundFade(bool fadeIn, AudioSource source, float duration, float endVolume, SoundEnum type)
    {
        if (!fadeIn)
        {
            //double lengthofSource = (double)source.clip.samples / source.clip.frequency; // ��ü ��� ���� 
            yield return new WaitForSeconds((float)(source.clip.length - duration));
        }
        
        float time = 0f;
        float startVolume = source.volume;

        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, endVolume, time / duration);
            yield return null;
        }

        if (!fadeIn)
            Play(source.clip, type);

        yield break;
    }
}
