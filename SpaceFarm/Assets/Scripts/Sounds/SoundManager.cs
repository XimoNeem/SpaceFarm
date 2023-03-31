using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource[] _audioSources = new AudioSource[3];

    [SerializeField] private PresetsSound[] _presetsSounds;

    [SerializeField] [Range(0, 1)] private float _volume = 0.5f;
    [SerializeField] [Range(1, 3f)] private float _maxPitch = 1f;
    [SerializeField] [Range(-3f, 1)] private float _minPitch = 1f;

    private int _currentIndex = 0;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
    }

    private void Start()
    {
        for (int i = 0; i < _audioSources.Length; i++)
        {
            _audioSources[i] = gameObject.AddComponent<AudioSource>();
            _audioSources[i].volume = _volume;
        }
    }

    public void PlaySound(AudioClip clip) 
    {
        _audioSources[_currentIndex].clip = clip;
        _audioSources[_currentIndex].pitch = Random.Range(_minPitch, _maxPitch);
        _audioSources[_currentIndex].Play();
        _currentIndex++;
        if (_currentIndex == _audioSources.Length) _currentIndex = 0;
    }

    public void PlaySound(SFXType type)
    {
        foreach (var item in _presetsSounds)
        {
            if (item.Type == type)
            {
                PlaySound(item.GetSound());
                
            }
        }
    }
}

public enum SFXType
{
    Click = 0,
    Close = 1,
    Purchase = 2,
}

[System.Serializable]
public class PresetsSound
{
    public SFXType Type;
    public AudioClip[] Clips;

    public AudioClip GetSound()
    {
        return Clips[Random.Range(0, Clips.Length)];
    }
}
