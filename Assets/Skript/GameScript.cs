using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public Canvas villageCanvas;
    public Canvas miniGameCanvas;
    public GameObject chestButton;
    public float chestCooldown = 10f;

    private float cooldownTimer;
    private bool isChestAvailable = true;

    void Start()
    {
        villageCanvas.gameObject.SetActive(true);
        miniGameCanvas.gameObject.SetActive(false);
        chestButton.SetActive(false);
        StartCooldown();
    }

    void Update()
    {
        if (!isChestAvailable)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                ShowChestButton();
            }
        }
    }

    public void OpenMiniGame()
    {
        if (!isChestAvailable) return;

        villageCanvas.gameObject.SetActive(false);
        miniGameCanvas.gameObject.SetActive(true);
        chestButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void CloseMiniGame()
    {
        villageCanvas.gameObject.SetActive(true);
        miniGameCanvas.gameObject.SetActive(false);
        StartCooldown();
        Time.timeScale = 1;
    }

    private void StartCooldown()
    {
        isChestAvailable = false;
        cooldownTimer = chestCooldown;
    }

    private void ShowChestButton()
    {
        chestButton.SetActive(true);
        isChestAvailable = true;
    }
}