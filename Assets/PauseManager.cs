using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [Header("Кнопки")]
    public Button pauseButton;    // Кнопка паузы
    public Button resumeButton;   // Кнопка продолжения

    private void Start()
    {
         pauseButton.onClick.AddListener(() => {
            AudioManager.Instance.PlayButtonClick(0); // Первый звук
            PauseGame();
        });

        resumeButton.onClick.AddListener(() => {
            AudioManager.Instance.PlayButtonClick(1); // Второй звук
            ResumeGame();
        });
        // Назначаем обработчики для кнопок
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        
        // Сначала показываем только кнопку паузы
        pauseButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);
    }

    // Метод для паузы игры
    public void PauseGame()
    {
        Time.timeScale = 0f; // Останавливаем игровое время
        
        // Переключаем видимость кнопок
        pauseButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
        
        Debug.Log("Игра на паузе");
    }

    // Метод для продолжения игры
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Возобновляем нормальное время
        
        // Переключаем видимость кнопок
        resumeButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        
        Debug.Log("Игра продолжается");
    }
}