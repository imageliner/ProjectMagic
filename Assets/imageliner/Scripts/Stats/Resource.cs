using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Resource
{
    public int baseValue = 1;

    public int totalValue { get; private set; }

    public int bonusValue { get; private set; } = 0;
    public int currentValue { get; private set; } = 1;

    public int GetTotalValue(Stat stat, int conversionValue)
    {
        totalValue = baseValue + (stat.baseValue * conversionValue) + bonusValue;
        return totalValue;
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
        if (currentValue + amount > totalValue )
            currentValue = totalValue;
        else
            currentValue += amount;
    }

    public void SubtractResource(int amount)
    {
        currentValue -= amount;
        if (currentValue < 0)
            currentValue = 0;
    }
}
