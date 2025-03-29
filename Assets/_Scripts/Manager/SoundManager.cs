using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SoundManager;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {  get; private set; }
    [SerializeField] private AudioSource _audioSource;
    [Header("Volume Settings")]
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float effectsVolume = 1f;
    [SerializeField] private List<AudioClip> _sfx;
    [SerializeField] private List<AudioClip> _music;
    [SerializeField] private Slider music;
    [SerializeField] private Slider sound;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void PlayButtonSound()
    {
        _audioSource.clip = _sfx[(int)SFX.Button];
        _audioSource.Play();
    }
    public void PlaySFX(SFX sFX) 
    {
        switch(sFX)
        {
            case(SFX.Attack):

                break;

            case (SFX.Correct):

                break;

            case (SFX.Button):
                _audioSource.clip = _sfx[(int)sFX];
                _audioSource.Play();
                break;
            case (SFX.None):
                
                break;
        }
    }
    public void PlayMusic()
    {

    }
    public enum SFX
    {
        None,
        Attack,
        Correct,
        Button,
    }
}
