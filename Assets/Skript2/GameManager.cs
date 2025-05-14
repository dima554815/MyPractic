using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ImageTimerHarvest HarvestTimer;
    public TimerEating EatingTimer;
    public Image RaidTimerImg;
    public Image PeasantTimerImg;
    public Image WarriorTimerImg;

    public Button peasantBatton;
    public Button warriorBatton;

    public TMPro.TextMeshProUGUI resourcesText;

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
    private float peasantTimer = -2;
    private float warriorTimer = -2;
    private float raidTimer;

    void Start()
    {
        UpdateText();
        raidTimer = raidMaxTime;
        UpdateButtonsInteractable();
    }

    void Update()
    {
        raidTimer -= Time.deltaTime;
        RaidTimerImg.fillAmount = raidTimer / raidMaxTime;

        if(raidTimer <=0)
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
            peasantBatton.interactable = true;
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
            warriorBatton.interactable = true;
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
            peasantBatton.interactable = false;
            UpdateButtonsInteractable();
        }
    }

    public void CreateWarrior()
    {
        if (wheatCount >= warriorCost)
        {
            wheatCount -= warriorCost;
            warriorTimer = warriorCreateTime;
            warriorBatton.interactable = false;
            UpdateButtonsInteractable();
        }
    }

    private void UpdateText()
    {
        resourcesText.text = peasantCount + "\n" + warriorsCount + "\n\n" + wheatCount;
    }

    private void UpdateButtonsInteractable()
    {
        // Проверяем, достаточно ли пшеницы для найма и не идет ли уже процесс найма
        peasantBatton.interactable = wheatCount >= peasantCost && peasantTimer <= -1;
        warriorBatton.interactable = wheatCount >= warriorCost && warriorTimer <= -1;
    }
}