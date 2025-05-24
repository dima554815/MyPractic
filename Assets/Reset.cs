using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public void RestartGame()
    {
        // 1. Восстанавливаем нормальную скорость игры
        Time.timeScale = 1f;
        
        // 2. Перезагружаем текущую сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}