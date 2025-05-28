using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Название сцены с игрой (указать в Inspector!)
    public string gameSceneName = "GameScene"; 

    // Метод для кнопки "Играть"
    public void OnPlayButtonClicked()
    {
        // Проигрываем звук (если есть AudioManager)
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick(0);
        
        // Загружаем игровую сцену
        SceneManager.LoadScene(gameSceneName);
    }

    // Метод для кнопки "Выйти"
    public void OnExitButtonClicked()
    {
        // Проигрываем звук
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick(1);
        
        // Возвращаемся в главное меню (если ты уже в игре)
        // Или закрываем игру (если это десктоп)
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}