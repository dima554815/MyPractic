using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Timers")]
    public ImageTimerHarvest HarvestTimer;
    public TimerEating EatingTimer;
    public Image RaidTimerImg;
    public Image PeasantTimerImg;
    public Image WarriorTimerImg;

    [Header("Buttons")]
    public Button peasantButton;
    public Button warriorButton;
    public Button peasantUpgradeButton;
    public Button warriorUpgradeButton;
    public Button harvestUpgradeButton;

    [Header("UI Text")]
    public TMP_Text resourcesText;
    public TMP_Text peasantUpgradeCostText;
    public TMP_Text warriorUpgradeCostText;
    public TMP_Text harvestUpgradeCostText;

    [Header("Game Parameters")]
    public int peasantCount;
    public int warriorsCount;
    public int wheatCount;

    public int wheatPerPeasant;
    public int wheatToWarriors;

    public int peasantCost;
    public int warriorCost;

    public float peasantCreateTime;
    public float warriorCreateTime;
    public float raidMaxTime;
    public int raidIncrease;
    public int nextRaid;
    public GameObject GameOverScreen;

    [Header("Upgrade System")]
    public int peasantSpeedUpgradeLevel = 0;
    public int warriorSpeedUpgradeLevel = 0;
    public int harvestSpeedUpgradeLevel = 0;
    public int peasantSpeedUpgradeCost = 50;
    public int warriorSpeedUpgradeCost = 50;
    public int harvestSpeedUpgradeCost = 50;
    public float peasantSpeedUpgradeAmount = 0.1f;
    public float warriorSpeedUpgradeAmount = 0.1f;
    public float harvestSpeedUpgradeAmount = 0.1f;

    private float peasantTimer = -2;
    private float warriorTimer = -2;
    private float raidTimer;
   
    void Start()
    {
        UpdateText();
        UpdateUpgradeCostTexts();
        raidTimer = raidMaxTime;
        UpdateButtonsInteractable();
       
    }

    void Update()
    {
        raidTimer -= Time.deltaTime;
        RaidTimerImg.fillAmount = raidTimer / raidMaxTime;

        if(raidTimer <= 0)
        {
            raidTimer = raidMaxTime;
            warriorsCount -= nextRaid;
            nextRaid += raidIncrease;
        }
          
        if(HarvestTimer.Tick)
        {
            wheatCount += peasantCount * wheatPerPeasant;
            UpdateButtonsInteractable();
        }

        if(EatingTimer.Tick)
        {
            wheatCount -= warriorsCount * wheatToWarriors;
            UpdateButtonsInteractable();
        }

        if(peasantTimer > 0)
        {
            peasantTimer -= Time.deltaTime;
            PeasantTimerImg.fillAmount = peasantTimer / peasantCreateTime;
        }
        else if(peasantTimer > -1)
        {
            PeasantTimerImg.fillAmount = 1;
            peasantButton.interactable = true;
            peasantCount += 1;
            peasantTimer = -2;
            UpdateButtonsInteractable();
        }

        if(warriorTimer > 0)
        {
            warriorTimer -= Time.deltaTime;
            WarriorTimerImg.fillAmount = warriorTimer / warriorCreateTime;
        }
        else if (warriorTimer > -1)
        {
            WarriorTimerImg.fillAmount = 1;
            warriorButton.interactable = true;
            warriorsCount += 1;
            warriorTimer = -2;
            UpdateButtonsInteractable();
        }

        UpdateText();

        if(warriorsCount < 0)
        {
            Time.timeScale = 0;
            GameOverScreen.SetActive(true);
        }
    }

    public void CreatePeasant()
    {
        if (wheatCount >= peasantCost)
        {
            wheatCount -= peasantCost;
            peasantTimer = peasantCreateTime;
            peasantButton.interactable = false;
            UpdateButtonsInteractable();
        }
    }

    public void CreateWarrior()
    {
        if (wheatCount >= warriorCost)
        {
            wheatCount -= warriorCost;
            warriorTimer = warriorCreateTime;
            warriorButton.interactable = false;
            UpdateButtonsInteractable();
        }
    }

    public void UpgradePeasantSpeed()
    {
        if (wheatCount >= peasantSpeedUpgradeCost)
        {
            wheatCount -= peasantSpeedUpgradeCost;
            peasantCreateTime = Mathf.Max(0.5f, peasantCreateTime * (1 - peasantSpeedUpgradeAmount));
            peasantSpeedUpgradeLevel++;
            peasantSpeedUpgradeCost = (int)(peasantSpeedUpgradeCost * 1.5f);
            UpdateText();
            UpdateUpgradeCostTexts();
            UpdateButtonsInteractable();
        }
    }

    public void UpgradeWarriorSpeed()
    {
        if (wheatCount >= warriorSpeedUpgradeCost)
        {
            wheatCount -= warriorSpeedUpgradeCost;
            warriorCreateTime = Mathf.Max(0.5f, warriorCreateTime * (1 - warriorSpeedUpgradeAmount));
            warriorSpeedUpgradeLevel++;
            warriorSpeedUpgradeCost = (int)(warriorSpeedUpgradeCost * 1.5f);
            UpdateText();
            UpdateUpgradeCostTexts();
            UpdateButtonsInteractable();
        }
    }

    public void UpgradeHarvestSpeed()
    {
        if (wheatCount >= harvestSpeedUpgradeCost)
        {
            wheatCount -= harvestSpeedUpgradeCost;
            HarvestTimer.MaxTime = Mathf.Max(1f, HarvestTimer.MaxTime * (1 - harvestSpeedUpgradeAmount));
            harvestSpeedUpgradeLevel++;
            harvestSpeedUpgradeCost = (int)(harvestSpeedUpgradeCost * 1.5f);
            UpdateText();
            UpdateUpgradeCostTexts();
            UpdateButtonsInteractable();
        }
    }

    private void UpdateText()
    {
        resourcesText.text = 
            $"Крестьяне: {peasantCount}\n" +
            $"Воины: {warriorsCount}\n" +
            $"Зерно: {wheatCount}\n\n" +
            $"Lv. крестьян: {peasantSpeedUpgradeLevel}\n" +
            $"Lv. воинов: {warriorSpeedUpgradeLevel}\n" +
            $"Lv. урожая: {harvestSpeedUpgradeLevel}";
    }

    private void UpdateUpgradeCostTexts()
    {
        peasantUpgradeCostText.text = $"ускорить: {peasantSpeedUpgradeCost} зерна";
        warriorUpgradeCostText.text = $"ускорить: {warriorSpeedUpgradeCost} зерна";
        harvestUpgradeCostText.text = $"ускорить: {harvestSpeedUpgradeCost} зерна";
    }

    private void UpdateButtonsInteractable()
    {
        // Кнопки найма
        peasantButton.interactable = wheatCount >= peasantCost && peasantTimer <= -1;
        warriorButton.interactable = wheatCount >= warriorCost && warriorTimer <= -1;

        // Кнопки улучшений
        peasantUpgradeButton.interactable = wheatCount >= peasantSpeedUpgradeCost;
        warriorUpgradeButton.interactable = wheatCount >= warriorSpeedUpgradeCost;
        harvestUpgradeButton.interactable = wheatCount >= harvestSpeedUpgradeCost;
    }

   
}