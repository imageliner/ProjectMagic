using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public int baseValue { get; private set; } = 0;
    public int bonusValue { get; private set; } = 0;

    public int GetTotalValue()
    {
        return baseValue + bonusValue;
    }

    public void SetBaseValue(int value)
    {
        baseValue = value;
    }

    public void SetBonusValue(int value)
    {
        bonusValue = value;
    }

    public void AddBaseValue(int value)
    {
        baseValue += value;
    }

    public void AddBonusValue(int value)
    {
        bonusValue += value;
    }
}
