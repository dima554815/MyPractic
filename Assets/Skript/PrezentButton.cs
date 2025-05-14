using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrezentButton : MonoBehaviour
{
    [Header("Ссылки на канвасы")]
    public GameObject villageCanvas;
    public GameObject chestGameCanvas;

    [Header("Кнопка мини-игры (сундук)")]
    public GameObject chestButton;

    [Header("Задержка")]
    public float chestButtonDelay = 60f;

    void Start()
    {
        chestGameCanvas.SetActive(false);
        chestButton.SetActive(false);
        Invoke("ShowChestButton", 10f); // Первое появление через 10 сек
    }

    public void ShowChestButton()
    {
        chestButton.SetActive(true);
    }

    public void OpenChestGame()
    {
        villageCanvas.SetActive(false);
        chestGameCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseChestGame()
    {
        chestGameCanvas.SetActive(false);
        villageCanvas.SetActive(true);
        Time.timeScale = 1f;

        chestGameCanvas.GetComponent<LockPickingGame>().ResetGame();
        chestButton.SetActive(false);

        Debug.Log("Таймер запущен");

        // Кнопка снова появится через указанное время
        Invoke("ShowChestButton", chestButtonDelay);
    }
}