using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject mainMenuCanvas;
    public GameObject gameCanvas;
    public Button playButton;
    public Button exitButton;
    public Button returnToMenuButton;

    [Header("Game Objects")]
    public GameObject[] gameObjectsToReset; // Все объекты, которые нужно перезапускать
    private Vector3[] initialPositions;
    private Quaternion[] initialRotations;

    void Start()
    {
        // Сохраняем начальные позиции всех объектов
        SaveInitialStates();

        // Настройка кнопок
        playButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
        returnToMenuButton.onClick.AddListener(ReturnToMainMenu);

        // Начальное состояние
        ShowMainMenu();
    }

    void SaveInitialStates()
    {
        initialPositions = new Vector3[gameObjectsToReset.Length];
        initialRotations = new Quaternion[gameObjectsToReset.Length];
        
        for(int i = 0; i < gameObjectsToReset.Length; i++)
        {
            if(gameObjectsToReset[i] != null)
            {
                initialPositions[i] = gameObjectsToReset[i].transform.position;
                initialRotations[i] = gameObjectsToReset[i].transform.rotation;
            }
        }
    }

    void ResetGameObjects()
    {
        for(int i = 0; i < gameObjectsToReset.Length; i++)
        {
            if(gameObjectsToReset[i] != null)
            {
                gameObjectsToReset[i].transform.position = initialPositions[i];
                gameObjectsToReset[i].transform.rotation = initialRotations[i];
                
                // Дополнительный сброс для Rigidbody
                Rigidbody rb = gameObjectsToReset[i].GetComponent<Rigidbody>();
                if(rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }
    }

    void ShowMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        Time.timeScale = 0f; // Полная остановка времени
    }

    void StartGame()
    {
        mainMenuCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        ResetGameObjects(); // Полный сброс состояния
        Time.timeScale = 1f; // Нормальное время
    }

    void ReturnToMainMenu()
    {
        ShowMainMenu();
        ResetGameObjects(); // Полный сброс перед возвратом в меню
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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameCanvas.activeSelf)
            {
                ReturnToMainMenu();
            }
        }
    }
}