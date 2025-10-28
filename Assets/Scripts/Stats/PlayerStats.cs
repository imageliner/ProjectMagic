using TMPro;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    [SerializeField] int statPoints_perLevel = 2;
    [SerializeField] int statPoints_Offset = 0;
    [SerializeField] int statPoints_Spent = 0;

    [SerializeField] TextMeshProUGUI statPointText;
    [SerializeField] TextMeshProUGUI vitalityText;
    [SerializeField] TextMeshProUGUI healthText;


    public int statPoints { get; protected set; } = 0;

    private void Awake()
    {
        InitializeStats();
    }

    public void InitializeStats()
    {
        health = new Resource();
        health.SetBaseValue(100);
        health.SetCurrentValue(health.maxValue);
        vitality = new Stat();
        wisdom = new Stat();
        strength = new Stat();
        dexterity = new Stat();
        intelligence = new Stat();
    }

    public void SpendStatPoint()
    {
        if (statPoints > 0)
        {
            Debug.Log("spent stat point");
            statPoints--;
            statPoints_Spent++;

            //test
            
            OnVitalityUpgrade();
        }
    }

    public void OnUpdateLevel(int previousLevel, int currentLevel)
    {

        statPoints += statPoints_perLevel;

        vitalityText.text = $"Vitality: {vitality.GetTotalValue()}";
        healthText.text = $"Health: {maxHealth}";
        statPointText.text = $"Stat Points: {statPoints}";
    }

    public void OnVitalityUpgrade()
    {
        vitality.AddBaseValue(1);
        //health.SetMaxValue(maxHealth);
        statPointText.text = $"Stat Points: {statPoints}";
        vitalityText.text = $"Vitality: {vitality.GetTotalValue()}";
        healthText.text = $"Health: {maxHealth}";
    }
}
