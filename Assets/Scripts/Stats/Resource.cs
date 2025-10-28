using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Resource
{
    public int baseValue = 1;

    public int maxValue { get; private set; }

    public int bonusValue { get; private set; } = 0;
    public int currentValue { get; private set; } = 1;

    public int GetMaxValue(Stat stat, int conversionValue)
    {
        maxValue = baseValue + (stat.baseValue * conversionValue) + bonusValue;
        return maxValue;
    }

    public void SetBaseValue(int value)
    {
        baseValue = value;
    }

    public void SetBonusValue(int value)
    {
        bonusValue = value;
    }
    public void SetCurrentValue(int value)
    {
        currentValue = value;
    }

    public void AddResource(int amount)
    {
        if (currentValue + amount > maxValue )
            currentValue = maxValue;
        else
            currentValue += amount;
    }

    public void SubtractResource(int amount)
    {
        if (currentValue - amount <= 0)
            currentValue = 0;
        else
            currentValue -= amount;
    }
}
