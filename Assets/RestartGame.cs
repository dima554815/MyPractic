using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    [SerializeField] private Button restartButton; // Кнопка перезапуска

    private void Start()
    {
        // Если кнопка не назначена в инспекторе, попробуем найти её автоматически
        if (restartButton == null)
        {
            restartButton = GetComponent<Button>();
            if (restartButton == null)
            {
                Debug.LogError("RestartButton не найден! Назначьте кнопку в инспекторе.");
                return;
            }
        }

        // Назначаем метод перезапуска на клик
        restartButton.onClick.AddListener(ReloadCurrentScene);
    }

    // Метод для перезагрузки текущей сцены
    public void ReloadCurrentScene()
    {
        // Получаем индекс текущей сцены
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        // Перезагружаем сцену
        SceneManager.LoadScene(currentSceneIndex);
        
        // Если нужно сбросить время (если используется Time.timeScale)
        Time.timeScale = 1f;
    }
}