using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExt : MonoBehaviour
{
    public GameObject villageCanvas;
    public GameObject chestGameCanvas;

    public void OpenChestGame()
    {
        villageCanvas.SetActive(false);
        chestGameCanvas.SetActive(true);
        Time.timeScale = 0f; // Пауза для деревни
    }

    public void CloseChestGame()
    {
        chestGameCanvas.SetActive(false);
        villageCanvas.SetActive(true);
        Time.timeScale = 1f; // Возобновить игру в деревне
    }
}
