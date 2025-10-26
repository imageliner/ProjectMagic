using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public int baseValue;
    public int bonusValue;

    public int GetSumValue()
    {
        return baseValue + bonusValue;
    }

    public void IncreaseBaseValue(int value)
    {
        baseValue += value;
    }

    public void IncreaseBonusValue(int value)
    {
        bonusValue += value;
    }
}
