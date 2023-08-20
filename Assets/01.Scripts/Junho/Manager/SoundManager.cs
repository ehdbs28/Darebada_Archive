using Core;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour, IManager
{
    public AudioClip _uiClickSoundClip;

    AudioSource[] _audioSources = new AudioSource[(int)SoundEnum.SOUNDCOUNT]; // �Ŵ��� ������ ����� �ҽ�(����Ŀ) ������ ģ����

    public void InitManager() // Awake
    {
        string[] soundNames = System.Enum.GetNames(typeof(SoundEnum));
        for (int i = 0; i < soundNames.Length - 1; i++)
        {
            GameObject go = new GameObject { name = soundNames[i] };
            _audioSources[i] = go.AddComponent<AudioSource>();
            _audioSources[i].playOnAwake = false;
            go.transform.parent = this.transform;
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
            AudioSource audioSource = _audioSources[(int)SoundEnum.BGM];

            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClips;
            audioSource.Play();
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
        Play(_uiClickSoundClip, SoundEnum.EFFECT);
    }
}
