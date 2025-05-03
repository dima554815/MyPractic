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
    public Image warriorTimerImg;

    public Button peasantBatton;
    public Button warriorBatton;

    public Text resourcesText;

    public int peasantCount;
    public int warriorsCount;
    public int wheatCount;

    public int wheatPerPeasant;
    public int wheatToWarriors;

    public int peasantCost;
    public int warriorsCost;

    public float peasantCreateTime;
    public float warriorCreateTime;
    public float raidMaxTime;
    public int raidIncrease;
    public int nextRaid;

    private float peasantTimer = -2;
    private float warriorCostTimer = -2;
    private float raidMaxTimer;

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
            wheatCount += warriorsCount * wheatToWarriors;
        }

        UpdateText();
    }

    public void CreatePeasant()
    {

    }

    public void CreateWarrior()
    {

    }

    private void UpdateText()
    {
        resourcesText.text = peasantCount + "\n" + warriorsCount + "\n\n" + wheatCount;
    }








}
