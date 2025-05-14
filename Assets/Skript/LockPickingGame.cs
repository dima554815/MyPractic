using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LockPickingGame : MonoBehaviour
{
    [Header("Настройки игры")]
    public int minPinValue = 0;
    public int maxPinValue = 9;
    private int[] targetPins = new int[3];
    public float initialTime = 30f;
    
    [Header("UI элементы")]
    public TMPro.TextMeshProUGUI[] pinTexts;
    public TMPro.TextMeshProUGUI timerText;
    public TMPro.TextMeshProUGUI targetPinsText;
    public GameObject winPanel;
    public GameObject losePanel;
    public Button resetButton;
    
    [Header("Инструменты")]
    public int[][] tools = {
        new int[] {1, -1, 0}, // Дрель
        new int[] {-1, 2, -1}, // Молоток
        new int[] {-1, 1, 1} // Отмычка
    };

    private int[] currentPins = new int[3];
    private float currentTime;
    private bool gameActive = true;
    private bool isTimerRunning = false;

    [Header("Настройка фона")]
    public Image backgroundImage;
    public Sprite defaultBackground;
    public Sprite winBackground;
    public Sprite loseBackground;
    

    void Start()
    {
        GenerateSolvableCombination();
        InitializeGame();
        resetButton.onClick.AddListener(ResetGame);
        SetBackground(winBackground);
    }

    void Update()
    {
        if (!gameActive || !isTimerRunning) return;
        
        currentTime -= Time.unscaledDeltaTime;
        timerText.text = "Время: " + Mathf.Max(0, Mathf.Ceil(currentTime)).ToString();
        
        if (currentTime <= 0)
        {
            LoseGame();
        }
    }

    private void GenerateSolvableCombination()
    {
        // 1. Генерируем целевую комбинацию
        for (int i = 0; i < 3; i++)
        {
            targetPins[i] = Random.Range(minPinValue, maxPinValue + 1);
        }

        // 2. Делаем несколько случайных ходов "назад"
        int stepsBack = Random.Range(1, 4);
        currentPins = (int[])targetPins.Clone();
        
        for (int i = 0; i < stepsBack; i++)
        {
            int toolIndex = Random.Range(0, tools.Length);
            ApplyToolReverse(tools[toolIndex]);
        }

        UpdateTargetText();
    }

    private void ApplyToolReverse(int[] tool)
    {
        for (int i = 0; i < 3; i++)
        {
            currentPins[i] = Mathf.Clamp(currentPins[i] - tool[i], minPinValue, maxPinValue);
        }
    }

    private void UpdateTargetText()
    {
        targetPinsText.text = $"Цель: {targetPins[0]} {targetPins[1]} {targetPins[2]}";
    }

    private void InitializeGame()
    {
        for (int i = 0; i < 3; i++)
        {
            UpdatePinText(i);
        }
        
        currentTime = initialTime;
        gameActive = true;
        isTimerRunning = true;
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    public void UseTool(int toolIndex)
    {
        if (!gameActive) return;
        
        for (int i = 0; i < 3; i++)
        {
            currentPins[i] = Mathf.Clamp(currentPins[i] + tools[toolIndex][i], minPinValue, maxPinValue);
            UpdatePinText(i);
        }
        
        CheckWinCondition();
    }

    private void UpdatePinText(int pinIndex)
    {
        pinTexts[pinIndex].text = currentPins[pinIndex].ToString();
    }

    private void CheckWinCondition()
    {
        bool win = true;
        for (int i = 0; i < 3; i++)
        {
            if (currentPins[i] != targetPins[i])
            {
                win = false;
                break;
            }
        }
        
        if (win)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        gameActive = false;
        isTimerRunning = false;
        winPanel.SetActive(true);
        SetBackground(winBackground);
    }

    private void LoseGame()
    {
        gameActive = false;
        isTimerRunning = false;
        losePanel.SetActive(true);
        SetBackground(loseBackground);
    }
    public void ResetGame()
    {
        isTimerRunning = false;
        GenerateSolvableCombination();
        InitializeGame();
        SetBackground(defaultBackground);
    }
    private void SetBackground(Sprite background)
    {
        if (backgroundImage != null && background != null)
        {
            backgroundImage.sprite = background;
        }
    }

    // Методы для кнопок инструментов
    public void UseDrill() => UseTool(0);
    public void UseHammer() => UseTool(1);
    public void UsePick() => UseTool(2);

    void OnEnable()
    {
        isTimerRunning = true;
    }

    void OnDisable()
    {
        isTimerRunning = false;
    }
}