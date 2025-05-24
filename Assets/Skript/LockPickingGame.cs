
/*public class LockPickingGame : MonoBehaviour
{
    public TextMeshProUGUI[] pinTexts;
    public TextMeshProUGUI timerText;
    public GameObject winPanel;
    public GameObject losePanel;
    public Button exitButton;

    private int[] targetPins = new int[3];
    private int[] currentPins = new int[3];
    private float currentTime;
    private bool gameActive;

    private void OnEnable()
    {
        StartNewGame();
    }

    private void StartNewGame()
    {
        // Генерация случайной комбинации
        for (int i = 0; i < 3; i++)
        {
            targetPins[i] = Random.Range(0, 10);
            currentPins[i] = Random.Range(0, 10);
            pinTexts[i].text = currentPins[i].ToString();
        }

        currentTime = 30f;
        gameActive = true;
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    private void Update()
    {
        if (!gameActive) return;

        currentTime -= Time.deltaTime;
        timerText.text = Mathf.Ceil(currentTime).ToString();

        if (currentTime <= 0) LoseGame();
    }

    public void UseTool(int[] toolEffects)
    {
        if (!gameActive) return;

        for (int i = 0; i < 3; i++)
        {
            currentPins[i] = Mathf.Clamp(currentPins[i] + toolEffects[i], 0, 9);
            pinTexts[i].text = currentPins[i].ToString();
        }

        CheckWin();
    }

    private void CheckWin()
    {
        for (int i = 0; i < 3; i++)
        {
            if (currentPins[i] != targetPins[i]) return;
        }
        WinGame();
    }

    private void WinGame()
    {
        gameActive = false;
        winPanel.SetActive(true);
    }

    private void LoseGame()
    {
        gameActive = false;
        losePanel.SetActive(true);
    }

    // Методы для кнопок (назначьте в инспекторе)
    public void UseDrill() => UseTool(new int[] {1, -1, 0});
    public void UseHammer() => UseTool(new int[] {-1, 2, -1});
    public void UsePick() => UseTool(new int[] {-1, 1, 1});
}*/
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LockPickingGame : MonoBehaviour
{
    [Header("Game Settings")]
    public int minPinValue = 0;
    public int maxPinValue = 9;
    public float gameDuration = 30f;

    [Header("UI Elements")]
    public TextMeshProUGUI[] pinTexts;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI targetPinsText;
    public GameObject winPanel;
    public GameObject losePanel;
    public Button exitButton;

    [Header("Backgrounds")]
    public Image backgroundImage;
    public Sprite defaultBackground;
    public Sprite winBackground;
    public Sprite loseBackground;

    [Header("Reward Display")]
    public TextMeshProUGUI warriorsRewardText;
    public TextMeshProUGUI peasantsRewardText;
    public TextMeshProUGUI wheatRewardText;

    // Инструменты и их эффекты
    private readonly int[][] tools = {
        new int[] {1, -1, 0},   // Дрель
        new int[] {-1, 2, -1},  // Молоток
        new int[] {-1, 1, 1}    // Отмычка
    };

    private int[] targetPins = new int[3];
    private int[] currentPins = new int[3];
    private float currentTime;
    private bool gameActive;

    private void OnEnable()
    {
        GenerateSolvableCombination();
        ResetGame();
    }

    private void GenerateSolvableCombination()
    {
        // 1. Генерация случайной цели (не только 555)
        for (int i = 0; i < 3; i++)
        {
            targetPins[i] = Random.Range(0, 9); // Цель от 3 до 6
        }

        // 2. Создаем решаемую стартовую позицию
        currentPins = (int[])targetPins.Clone();
        int stepsBack = Random.Range(1, 4); // 1-3 шага назад
        
        for (int i = 0; i < stepsBack; i++)
        {
            ApplyToolReverse(tools[Random.Range(0, tools.Length)]);
        }

        targetPinsText.text = $"Цель: {targetPins[0]} {targetPins[1]} {targetPins[2]}";
    }

    private void ApplyToolReverse(int[] tool)
    {
        for (int i = 0; i < 3; i++)
        {
            currentPins[i] = Mathf.Clamp(currentPins[i] - tool[i], minPinValue, maxPinValue);
        }
    }

    public void ResetGame()
    {
        currentTime = gameDuration;
        gameActive = true;
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        SetBackground(defaultBackground);

        for (int i = 0; i < 3; i++)
        {
            pinTexts[i].text = currentPins[i].ToString();
        }
    }

    private void Update()
    {
        if (!gameActive) return;

        currentTime -= Time.unscaledDeltaTime;
        timerText.text = Mathf.Ceil(currentTime).ToString();

        if (currentTime <= 0) LoseGame();
    }

    public void UseTool(int toolIndex)
    {
        if (!gameActive) return;

        for (int i = 0; i < 3; i++)
        {
            currentPins[i] = Mathf.Clamp(currentPins[i] + tools[toolIndex][i], minPinValue, maxPinValue);
            pinTexts[i].text = currentPins[i].ToString();
        }

        CheckWin();
    }

    private void CheckWin()
    {
        for (int i = 0; i < 3; i++)
        {
            if (currentPins[i] != targetPins[i]) return;
        }
        WinGame();
    }

    private void WinGame()
    {
        gameActive = false;
        winPanel.SetActive(true);
        SetBackground(winBackground);
    }

    private void LoseGame()
    {
        gameActive = false;
        losePanel.SetActive(true);
        SetBackground(loseBackground);

         // Генерируем награды
        warriorsReward = Random.Range(1, 6); // 1-5 воинов
        peasantsReward = Random.Range(1, 4); // 1-3 крестьян
        wheatReward = Random.Range(10, 31); // 10-30 пшеницы
    
        // Отображаем награды
        warriorsRewardText.text = "+" + warriorsReward;
        peasantsRewardText.text = "+" + peasantsReward;
        wheatRewardText.text = "+" + wheatReward;
    }

    // Вызывается при нажатии кнопки "Забрать награду"
    public void ClaimReward()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.warriorsCount += warriorsReward;
            gameManager.peasantCount += peasantsReward;
            gameManager.wheatCount += wheatReward;
        
            gameManager.UpdateText();
            gameManager.UpdateButtonsInteractable();
        }
    
        // Закрываем мини-игру
        FindObjectOfType<GameScript>().CloseMiniGame();
    }

    private void SetBackground(Sprite background)
    {
        if (backgroundImage != null && background != null)
        {
            backgroundImage.sprite = background;
        }
    }

    public void CloseMiniGame()
    {
        FindObjectOfType<GameScript>().CloseMiniGame();
    }

    // Методы для кнопок (назначьте в инспекторе!)
    public void UseDrill() => UseTool(0);
    public void UseHammer() => UseTool(1);
    public void UsePick() => UseTool(2);
}