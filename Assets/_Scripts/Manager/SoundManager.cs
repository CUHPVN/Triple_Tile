using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SoundManager;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {  get; private set; }
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float musicVolume = 0.5f;
    [Range(0f, 1f)] public float effectsVolume = 0.5f;
    [SerializeField] private List<AudioClip> _sfx;
    [SerializeField] private List<AudioClip> _music;
    [SerializeField] private Slider music;
    [SerializeField] private Slider sound;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (music != null && music.gameObject.activeSelf)
        {
            musicVolume = music.value;
            effectsVolume = sound.value;
            ChangeVolume();
        }
        if (_musicSource.isPlaying == false)
        {
            PlayMusic();
        }
    }
    public void LoadMusicSlider(Slider slider)
    {
        music = slider;
    }
    public void LoadSFXSlider(Slider slider)
    {
        sound = slider;
    }
    public void PlayButtonSound()
    {
        _sfxSource.PlayOneShot(_sfx[(int)SFX.Button]);
    }
    public void ChangeVolume()
    {
        _musicSource.volume = musicVolume;
        _sfxSource.volume = effectsVolume;
    }
    public float GetMusicVolume()
    {
        return musicVolume;
    }
    public float GetEffectsVolume()
    {
        return effectsVolume;
    }
    public void PlaySFX(SFX sFX) 
    {
        _sfxSource.PlayOneShot(_sfx[(int)sFX]);
    }
    public void PlayMusic()
    {
        int random = Random.Range(0, _music.Count);
        _musicSource.clip = _music[random];
        _musicSource.Play();
    }
    public enum SFX
    {
        None,
        Attack,
        Correct,
        Button,
        Win,
    }
}
