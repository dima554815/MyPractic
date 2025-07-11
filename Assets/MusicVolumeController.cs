using UnityEngine;
using UnityEngine.Audio;

public class MusicVolumeController : MonoBehaviour
{
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioMixer audioMixer; // Опционально, если используете AudioMixer
    
    // Название параметра громкости в AudioMixer (если используется)
    private const string MixerVolumeParam = "MusicVolume";
    
    // Минимальное и максимальное значение громкости (в децибелах)
    private const float MinVolumeDB = -80f;
    private const float MaxVolumeDB = 0f;
    
    // Текущая громкость (линейное значение от 0 до 1)
    private float currentVolume = 0.5f;
    
    private void Start()
    {
        // Инициализация громкости
        SetVolume(currentVolume);
    }
    
    // Метод для установки громкости (значение от 0 до 1)
    public void SetVolume(float volume)
    {
        currentVolume = Mathf.Clamp01(volume);
        
        // Если используется прямой AudioSource
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = currentVolume;
        }
        
        // Если используется AudioMixer (предпочтительный способ)
        if (audioMixer != null)
        {
            // Конвертируем линейное значение в децибелы
            float volumeDB = Mathf.Lerp(MinVolumeDB, MaxVolumeDB, currentVolume);
            audioMixer.SetFloat(MixerVolumeParam, volumeDB);
        }
    }
    
    // Метод для увеличения/уменьшения громкости на определенное значение
    public void ChangeVolume(float delta)
    {
        SetVolume(currentVolume + delta);
    }
    
    // Свойство для получения текущей громкости
    public float CurrentVolume => currentVolume;
}
