using UnityEngine;
using UnityEngine.UI;

public class PresentBatton : MonoBehaviour
{
    public GameObject villageGameCanvas;
    public GameObject chestGameCanvas;
    public Button chestButton;

    private float chestCooldown = 10f; // 1 минута
    private float timer;
    private bool isChestGameActive = false;

    void Start()
    {
        chestButton.onClick.AddListener(OpenChestGame);
        chestButton.gameObject.SetActive(false);
        chestGameCanvas.SetActive(false);
        villageGameCanvas.SetActive(true);

        Time.timeScale = 1f;
        timer = chestCooldown;
    }

    void Update()
    {
        if (!isChestGameActive)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                chestButton.gameObject.SetActive(true);
            }
        }
    }

    void OpenChestGame()
    {
        // Останавливаем игру в деревне
        //Time.timeScale = 0f;
        //isChestGameActive = true;

        // Показываем мини-игру
        chestGameCanvas.SetActive(true);
        villageGameCanvas.SetActive(false);

        chestButton.gameObject.SetActive(false);
        timer = chestCooldown;
    }

    public void CloseChestGame()
    {
        // Возвращаемся к деревне
        Time.timeScale = 1f;
        isChestGameActive = false;

        chestGameCanvas.SetActive(false);
        villageGameCanvas.SetActive(true);
    }
}
