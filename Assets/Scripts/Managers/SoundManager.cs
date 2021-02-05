using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{   
    //Singleton
    public static SoundManager instance;

    //AudioMixer
    private AudioMixer audioMixer;
    public AudioMixer AudioMixer { get => audioMixer; set => audioMixer = value; }

    //AudioSources
    private AudioSource _backgroundSource = null;
    public AudioSource BackgroundSource { get => _backgroundSource; set => _backgroundSource = value; }
    private AudioSource _sfxSource = null;
    public AudioSource SfxSource { get => _sfxSource; set => _sfxSource = value; }
    private AudioSource _masterSource;
    public AudioSource MasterSource { get => _masterSource; set => _masterSource = value; }

    //Background AudioClip
    private AudioClip _backgroundAudioClip = null;
    public AudioClip BackgroundAudioClip { get => _backgroundAudioClip; set => _backgroundAudioClip = value; }
    //Sfx AudioClip
    private AudioClip _sfxAudioClip = null;
    public AudioClip SfxAudioClip { get => _sfxAudioClip; set => _sfxAudioClip = value; }

    //Features
    private bool isActiveMuteSfx = false;
    private bool isActiveMuteMusic = false;
    private float currentSfx = 0.0f;
    private float currentMusic = 0.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        AudioMixer = Resources.Load("MasterMixer") as AudioMixer;
        BackgroundSource = GetComponent<AudioSource>();
        SfxSource = GetComponent<AudioSource>();
        MasterSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        MasterSource.Play();
    }

    public void PlayBackground(string Route)
    {
        BackgroundAudioClip = Resources.Load(Route) as AudioClip;
        BackgroundSource.clip = BackgroundAudioClip;
        BackgroundSource.Play();
    }

    public void PlaySfx(string Route)
    {
        SfxAudioClip = Resources.Load(Route) as AudioClip;
        SfxSource.clip = SfxAudioClip;
        SfxSource.Play();
    }

    public void SetMusicVolumen(float volume)
    {
        AudioMixer.SetFloat("MusicVolumeParam", volume);
        PlayerPrefs.SetFloat("MusicVolumeLevel", volume);
    }

    public void SetSfxVolumen(float volume)
    {
        AudioMixer.SetFloat("SfxVolumeParam", volume);
        PlayerPrefs.SetFloat("SfxVolumeLevel", volume);
    }

    //Para la interfaz gráfica si es que se llega
    public void StartSliders(Slider musicSlider, Slider sfxSlider)
    {
        if (musicSlider != null && musicSlider != null)
        {
            musicSlider.value = PlayerPrefs.HasKey("MusicVolumeLevel") ?
            PlayerPrefs.GetFloat("MusicVolumeLevel") : -30f;
            SetMusicVolumen(musicSlider.value);
            sfxSlider.value = PlayerPrefs.HasKey("SfxVolumeLevel") ?
            PlayerPrefs.GetFloat("SfxVolumeLevel") : -30f;
            SetSfxVolumen(sfxSlider.value);
        }
    }

    public void MuteMusicVolumen(Slider musicSlider)
    {
        if (!isActiveMuteMusic)
        {
            currentMusic = PlayerPrefs.GetFloat("MusicVolumeLevel");
            SetMusicVolumen(musicSlider.minValue);
            musicSlider.value = musicSlider.minValue;
            isActiveMuteMusic = true;
        }
        else
        {
            SetMusicVolumen(currentMusic);
            musicSlider.value = currentMusic;
            isActiveMuteMusic = false;
        }
    }

    public void MuteSfxVolume(Slider sfxSlider)
    {
        if (!isActiveMuteSfx)
        {
            currentSfx = PlayerPrefs.GetFloat("SfxVolumeLevel");
            SetSfxVolumen(sfxSlider.minValue);
            sfxSlider.value = sfxSlider.minValue;
            isActiveMuteSfx = true;
        }
        else
        {
            SetSfxVolumen(currentSfx);
            sfxSlider.value = currentSfx;
            isActiveMuteSfx = false;
        }
    }
}
