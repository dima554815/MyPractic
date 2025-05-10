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

    private float peasantTimer = -2;
    private float warriorTimer = -2;
    private float raidTimer;

    void Start()
    {
        UpdateText();
    }

    void Update()
    {
        if(HarvestTimer.Tick)
        {
            wheatCount += peasantCount * wheatPerPeasant;
        }

        if(EatingTimer.Tick)
        {
            wheatCount -= warriorsCount * wheatToWarriors;
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
        }

        UpdateText();
    }

    public void CreatePeasant()
    {
        wheatCount -= peasantCost;
        peasantTimer = peasantCreateTime;
        peasantBatton.interactable = false;
    }

    public void CreateWarrior()
    {
        wheatCount -= warriorCost;
        warriorTimer = warriorCreateTime;
        warriorBatton.interactable = false;
    }

    private void UpdateText()
    {
        resourcesText.text = peasantCount + "\n" + warriorsCount + "\n\n" + wheatCount;
    }








}
