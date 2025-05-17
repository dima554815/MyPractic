using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    [Header("Канвасы")]
    public GameObject villageCanvas;
    public GameObject miniGameCanvas;

    [Header("Кнопка запуска мини-игры")]
    public Button chestButton;

    [Header("Выход из мини-игры")]
    public Button exitMiniGameButton;

    [Header("Задержка появления кнопки сундука")]
    public float chestButtonDelay = 10f;

    private LockPickingGame lockPickingGame;

    void Start()
    {
        // Скрыть мини-игру и кнопку в начале
        miniGameCanvas.SetActive(false);
        chestButton.gameObject.SetActive(false);

        // Скрыть мини-игру, показать деревню
        villageCanvas.SetActive(true);

        // Найти скрипт мини-игры
        lockPickingGame = miniGameCanvas.GetComponentInChildren<LockPickingGame>();

        // Назначить кнопки
        chestButton.onClick.AddListener(OpenMiniGame);
        exitMiniGameButton.onClick.AddListener(CloseMiniGame);

        // Запустить таймер появления сундука
        Invoke(nameof(EnableChestButton), chestButtonDelay);
    }

    void EnableChestButton()
    {
        chestButton.gameObject.SetActive(true);
    }

    void OpenMiniGame()
    {
        // Включить мини-игру, отключить деревню
        miniGameCanvas.SetActive(true);
        villageCanvas.SetActive(false);
        chestButton.gameObject.SetActive(false);

        // Запустить мини-игру
        if (lockPickingGame != null)
            lockPickingGame.StartMiniGame();
    }

    void CloseMiniGame()
    {
        // Остановить мини-игру
        if (lockPickingGame != null)
            lockPickingGame.ResetGame();

        // Включить деревню, отключить мини-игру
        miniGameCanvas.SetActive(false);
        villageCanvas.SetActive(true);

        // Появление кнопки сундука через задержку
        Invoke(nameof(EnableChestButton), chestButtonDelay);
    }
}