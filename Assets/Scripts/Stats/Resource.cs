using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Resource
{
    public int maxValue { get; private set; } = 1;
    public int bonusValue { get; private set; } = 0;
    public int currentValue { get; private set; } = 1;

    public int GetTotalValue()
    {
        return maxValue + bonusValue;
    }

    public void SetMaxValue(int value)
    {
        maxValue = value;
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
