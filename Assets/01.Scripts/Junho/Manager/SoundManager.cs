using Core;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour, IManager
{
    private static SoundManager _instance;
    public static SoundManager Instance // ����ȭ ���� ������ ������
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<SoundManager>();
                }
            }

            return _instance;
        }
    }

    AudioSource[] _audioSources = new AudioSource[(int)SoundEnum.SOUNDCOUNT]; // �Ŵ��� ������ ����� �ҽ�(����Ŀ) ������ ģ����

    public void InitManager() // Awake
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            string[] soundNames = System.Enum.GetNames(typeof(SoundEnum));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = this.transform;
            }

            _audioSources[(int)SoundEnum.BGM].loop = true;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public void ResetManager()
    {
        foreach (var audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
    }

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
}
