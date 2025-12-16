using System;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    [SerializeField] private int statPoints_perLevel = 2;
    [SerializeField] private int statPoints_Offset = 0;
    [SerializeField] private int statPoints_Spent = 0;

    private Stat[] allStats;

    public Action StatsUpdated;

    public static Action hasStatPoints;
    public static Action noStatPoints;

    public int statPoints { get; protected set; } = 0;

    protected override void Awake()
    {
        base.Awake();
        allStats = new Stat[] { vitality, wisdom, strength, dexterity, intelligence, pAtk, pDef, mAtk, mDef, atkSpeed };
    }

    public enum StatType
    {
        Vitality,
        Wisdom,
        Strength,
        Dexterity,
        Intelligence
    }

    public void ResetStats()
    {

        foreach (Stat stat in allStats)
        {
            stat.SetBaseValue(0);
            stat.SetBonusValue(0);
        }

        statPoints_Spent = 0;
        statPoints = 2;

        StatsUpdated?.Invoke();

        health.SetCurrentValue(initialHealth);
        mana.SetCurrentValue(initialMana);
    }

    protected override void InitializeStats()
    {
        base.InitializeStats();
    }

    public void SpendStatPoint(StatType statType)
    {
        if (statPoints <= 0)
        {
            Debug.Log("not enough stat points");
            return;
        }

        statPoints--;
        statPoints_Spent++;

        if (statPoints > 0)
            noStatPoints?.Invoke();

        switch (statType)
        {
            case StatType.Vitality:
                vitality.AddBaseValue(1);
                break;

            case StatType.Wisdom:
                wisdom.AddBaseValue(1);
                break;

            case StatType.Strength:
                strength.AddBaseValue(1);
                break;

            case StatType.Dexterity:
                dexterity.AddBaseValue(1);
                break;

            case StatType.Intelligence:
                intelligence.AddBaseValue(1);
                break;

            default:
                Debug.LogWarning("Unknown stat type in SpendStatPoint");
                break;
        }
        SoundManager.singleton.PlayAudio(SoundManager.singleton.sfx_UseItem);

        StatsUpdated?.Invoke();
    }

    public void OnUpdateLevel(int previousLevel, int currentLevel)
    {

        statPoints += statPoints_perLevel;

        StatsUpdated?.Invoke();
        hasStatPoints?.Invoke();

    }
}
