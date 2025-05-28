using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject mainMenuCanvas;
    public GameObject gameCanvas;
    public Button playButton;
    public Button exitButton;
    public Button returnToMenuButton;

    [Header("Game Settings")]
    public string gameSceneName = "GameScene";
    private bool isGameActive = false;

    void Start()
    {
        // Настройка кнопок
        playButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
        returnToMenuButton.onClick.AddListener(ReturnToMainMenu);

        // Начальное состояние
        ShowMainMenu();
    }

    void ShowMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        Time.timeScale = 0; // Останавливаем игровое время
        isGameActive = false;
    }

    void StartGame()
    {
        // Вариант 1: Если у вас одна сцена
        mainMenuCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        Time.timeScale = 1; // Возобновляем время
        isGameActive = true;

        // Вариант 2: Если нужно перезагружать сцену
        //SceneManager.LoadScene(gameSceneName);
        //Time.timeScale = 1;
    }

    void ReturnToMainMenu()
    {
        // Вариант 1: Для одной сцены
        ResetGameState();
        ShowMainMenu();

        // Вариант 2: Для перезагрузки сцены
        //SceneManager.LoadScene(mainMenuSceneName);
        //Time.timeScale = 0;
    }

    void ResetGameState()
    {
        // Здесь добавьте сброс всех игровых параметров
        // Например:
        // score = 0;
        // player.ResetPosition();
        // enemyManager.ResetEnemies();
    }

    void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void Update()
    {
        // Пауза по ESC, только когда игра активна
        if (isGameActive && Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }
    }
}