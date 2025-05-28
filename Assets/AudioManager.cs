using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("UI Sounds")]
    public AudioClip[] buttonClickSounds;
    public AudioClip[] buttonHoverSounds;
    public AudioSource uiAudioSource;

    [Header("Volume Keys")]
    private const string MusicVolumeKey = "MusicVolume";
    private const string SfxVolumeKey = "SFXVolume";
    private const string UiVolumeKey = "UIVolume";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Загружаем сохраненные настройки громкости
            LoadVolumes();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayButtonClick(int soundIndex = -1)
    {
        if (buttonClickSounds.Length == 0) return;
        
        int index = soundIndex == -1 ? Random.Range(0, buttonClickSounds.Length) : Mathf.Clamp(soundIndex, 0, buttonClickSounds.Length - 1);
        uiAudioSource.PlayOneShot(buttonClickSounds[index]);
    }

    public void PlayButtonHover(int soundIndex = -1)
    {
        if (buttonHoverSounds.Length == 0) return;
        
        int index = soundIndex == -1 ? Random.Range(0, buttonHoverSounds.Length) : Mathf.Clamp(soundIndex, 0, buttonHoverSounds.Length - 1);
        uiAudioSource.PlayOneShot(buttonHoverSounds[index]);
    }

    // Методы для управления громкостью
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(MusicVolumeKey, LinearToDecibel(volume));
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(SfxVolumeKey, LinearToDecibel(volume));
        PlayerPrefs.SetFloat(SfxVolumeKey, volume);
    }

    public void SetUIVolume(float volume)
    {
        audioMixer.SetFloat(UiVolumeKey, LinearToDecibel(volume));
        PlayerPrefs.SetFloat(UiVolumeKey, volume);
        uiAudioSource.volume = volume;
    }

    private void LoadVolumes()
    {
        SetMusicVolume(PlayerPrefs.GetFloat(MusicVolumeKey, 0.7f));
        SetSFXVolume(PlayerPrefs.GetFloat(SfxVolumeKey, 0.8f));
        SetUIVolume(PlayerPrefs.GetFloat(UiVolumeKey, 0.8f));
    }

    private float LinearToDecibel(float linear)
    {
        return linear <= 0 ? -80f : Mathf.Log10(linear) * 20f;
    }
}