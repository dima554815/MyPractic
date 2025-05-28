using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [Header("Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider uiSlider;

    private void Start()
    {
        // Инициализация значений
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.8f);
        uiSlider.value = PlayerPrefs.GetFloat("UIVolume", 0.8f);
        
        // Подписываемся на изменения
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        uiSlider.onValueChanged.AddListener(SetUIVolume);
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.Instance.SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        AudioManager.Instance.SetSFXVolume(volume);
    }

    public void SetUIVolume(float volume)
    {
        AudioManager.Instance.SetUIVolume(volume);
    }
}