using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Основные ресурсы
    public int peasantCount = 3;
    public int warriorsCount = 1;
    public int wheatCount = 10;
    public TMP_Text peasantsText;
    public TMP_Text warriorsText;
    public TMP_Text wheatText;

    // Таймеры и улучшения
    public ImageTimerHarvest harvestTimer;
    public TimerEating eatingTimer;
    public Image raidTimerImage;
    public float raidMaxTime = 30f;
    private float raidTimer;
    public int nextRaid = 1;
    public int raidIncrease = 1;

    // Система найма
    public Button hirePeasantButton;
    public Button hireWarriorButton;
    public Image peasantTimerImage;
    public Image warriorTimerImage;
    public float peasantCreateTime = 5f;
    public float warriorCreateTime = 7f;
    private float peasantTimer = 0f;
    private float warriorTimer = 0f;
    public int peasantCost = 10;
    public int warriorCost = 15;

    // Система улучшений
    public Button upgradePeasantButton;
    public Button upgradeWarriorButton;
    public Button upgradeHarvestButton;
    public TMP_Text peasantLevelText;
    public TMP_Text warriorLevelText;
    public TMP_Text harvestLevelText;
    public TMP_Text peasantUpgradeCostText;
    public TMP_Text warriorUpgradeCostText;
    public TMP_Text harvestUpgradeCostText;
    public int peasantUpgradeCost = 50;
    public int warriorUpgradeCost = 50;
    public int harvestUpgradeCost = 50;
    private int peasantLevel = 0;
    private int warriorLevel = 0;
    private int harvestLevel = 0;
    public float peasantUpgradeAmount = 0.1f;
    public float warriorUpgradeAmount = 0.1f;
    public float harvestUpgradeAmount = 0.1f;

    // Игровое время
    public TMP_Text gameTimeText;
    private float gameTime;

    // Экран проигрыша
    public GameObject gameOverScreen;
    public TMP_Text finalTimeText;
    public Button restartButton;

    void Start()


    {
         raidTimer = raidMaxTime;
    
        // Обработчики с звуками
        restartButton.onClick.AddListener(() => {
            AudioManager.Instance.PlayButtonClick(0);
            RestartGame();
        });

        hirePeasantButton.onClick.AddListener(() => {
            AudioManager.Instance.PlayButtonClick(2);
            HirePeasant();
        });

        hireWarriorButton.onClick.AddListener(() => {
            AudioManager.Instance.PlayButtonClick(1);
            HireWarrior();
        });

        upgradePeasantButton.onClick.AddListener(() => {
            AudioManager.Instance.PlayButtonClick(3);
            UpgradePeasant();
        });

        upgradeWarriorButton.onClick.AddListener(() => {
            AudioManager.Instance.PlayButtonClick(3);
            UpgradeWarrior();
        });

        upgradeHarvestButton.onClick.AddListener(() => {
            AudioManager.Instance.PlayButtonClick(3);
            UpgradeHarvest();
        });

        
       /* raidTimer = raidMaxTime;
        restartButton.onClick.AddListener(RestartGame);

        hirePeasantButton.onClick.AddListener(HirePeasant);
        hireWarriorButton.onClick.AddListener(HireWarrior);
        upgradePeasantButton.onClick.AddListener(UpgradePeasant);
        upgradeWarriorButton.onClick.AddListener(UpgradeWarrior);
        upgradeHarvestButton.onClick.AddListener(UpgradeHarvest);*/

        // Инициализация изображений таймеров
        peasantTimerImage.gameObject.SetActive(false);
        warriorTimerImage.gameObject.SetActive(false);
        
        UpdateAllUI();
    }

    void Update()
    {
        if (gameOverScreen.activeSelf) return;

        gameTime += Time.deltaTime;
        gameTimeText.text = $"Время в игре: {Mathf.FloorToInt(gameTime / 60):00}:{Mathf.FloorToInt(gameTime % 60):00}";

        // Набеги
        raidTimer -= Time.deltaTime;
        raidTimerImage.fillAmount = raidTimer / raidMaxTime;
        if (raidTimer <= 0)
        {
            raidTimer = raidMaxTime;
            warriorsCount -= nextRaid;
            nextRaid += raidIncrease;
            UpdateAllUI();
        }

        // Обновление таймеров найма
        UpdateHireTimers();

        // Экономика
        if (harvestTimer.Tick)
        {
            wheatCount += peasantCount * 2;
            UpdateAllUI();
        }

        if (eatingTimer.Tick)
        {
            wheatCount -= warriorsCount;
            UpdateAllUI();
        }

        // Проверка поражения
        if (warriorsCount < 0)
        {
            GameOver();
        }
    }

    void UpdateHireTimers()
    {
        // Таймер крестьян
        if (peasantTimer > 0)
        {
            peasantTimer -= Time.deltaTime;
            peasantTimerImage.fillAmount = peasantTimer / peasantCreateTime;
            peasantTimerImage.gameObject.SetActive(true);
        }
        else if (peasantTimer <= 0 && peasantTimer > -1)
        {
            peasantCount++;
            peasantTimer = -2;
            UpdateAllUI();
        }

        // Таймер воинов
        if (warriorTimer > 0)
        {
            warriorTimer -= Time.deltaTime;
            warriorTimerImage.fillAmount = warriorTimer / warriorCreateTime;
            warriorTimerImage.gameObject.SetActive(true);
        }
        else if (warriorTimer <= 0 && warriorTimer > -1)
        {
            warriorsCount++;
            warriorTimer = -2;
            UpdateAllUI();
        }
    }

    public void UpdateAllUI()
    {
        // Ресурсы
        peasantsText.text = peasantCount.ToString();
        warriorsText.text = warriorsCount.ToString();
        wheatText.text = wheatCount.ToString();

        // Уровни улучшений
        peasantLevelText.text = $"Lv.крестьян {peasantLevel}";
        warriorLevelText.text = $"Lv.воинов {warriorLevel}";
        harvestLevelText.text = $"Lv.сбора {harvestLevel}";

        // Стоимость улучшений
        peasantUpgradeCostText.text = $"Цена {peasantUpgradeCost} пш.";
        warriorUpgradeCostText.text = $"Цена {warriorUpgradeCost} пш.";
        harvestUpgradeCostText.text = $"Цена {harvestUpgradeCost} пш.";

        // Кнопки найма
        hirePeasantButton.interactable = wheatCount >= peasantCost && peasantTimer <= 0;
        hireWarriorButton.interactable = wheatCount >= warriorCost && warriorTimer <= 0;

        // Кнопки улучшений
        upgradePeasantButton.interactable = wheatCount >= peasantUpgradeCost;
        upgradeWarriorButton.interactable = wheatCount >= warriorUpgradeCost;
        upgradeHarvestButton.interactable = wheatCount >= harvestUpgradeCost;
    }

    public void HirePeasant()
    {
        if (wheatCount >= peasantCost && peasantTimer <= 0)
        {
            wheatCount -= peasantCost;
            peasantTimer = peasantCreateTime;
            peasantTimerImage.gameObject.SetActive(true);
            UpdateAllUI();
        }
    }

    public void HireWarrior()
    {
        if (wheatCount >= warriorCost && warriorTimer <= 0)
        {
            wheatCount -= warriorCost;
            warriorTimer = warriorCreateTime;
            warriorTimerImage.gameObject.SetActive(true);
            UpdateAllUI();
        }
    }

    public void UpgradePeasant()
    {
        if (wheatCount >= peasantUpgradeCost)
        {
            wheatCount -= peasantUpgradeCost;
            peasantCreateTime = Mathf.Max(0.5f, peasantCreateTime * (1 - peasantUpgradeAmount));
            peasantLevel++;
            peasantUpgradeCost = (int)(peasantUpgradeCost * 1.5f);
            UpdateAllUI();
        }
    }

    public void UpgradeWarrior()
    {
        if (wheatCount >= warriorUpgradeCost)
        {
            wheatCount -= warriorUpgradeCost;
            warriorCreateTime = Mathf.Max(0.5f, warriorCreateTime * (1 - warriorUpgradeAmount));
            warriorLevel++;
            warriorUpgradeCost = (int)(warriorUpgradeCost * 1.5f);
            UpdateAllUI();
        }
    }

    public void UpgradeHarvest()
    {
        if (wheatCount >= harvestUpgradeCost)
        {
            wheatCount -= harvestUpgradeCost;
            harvestTimer.MaxTime = Mathf.Max(1f, harvestTimer.MaxTime * (1 - harvestUpgradeAmount));
            harvestLevel++;
            harvestUpgradeCost = (int)(harvestUpgradeCost * 1.5f);
            UpdateAllUI();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        finalTimeText.text = $"Итоговое время: {Mathf.FloorToInt(gameTime / 60):00}:{Mathf.FloorToInt(gameTime % 60):00}";
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}