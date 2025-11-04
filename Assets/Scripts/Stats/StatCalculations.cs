using UnityEngine;

[System.Serializable]
public class StatCalculations
{
    // physical attack range
    public int CalculatePhysAtkDmg(int physAtk)
    {
        int lowAtk = Mathf.CeilToInt(physAtk - (physAtk / 2));
        int highAtk = Mathf.CeilToInt(physAtk + (physAtk / 2));

        return Random.Range(lowAtk, highAtk);
    }

    // physical attack range
    public int CalculateMagAtkDmg(int magAtk)
    {
        int lowAtk = Mathf.CeilToInt(magAtk - (magAtk / 2));
        int highAtk = Mathf.CeilToInt(magAtk + (magAtk / 2));

        return Random.Range(lowAtk, highAtk);
    }
}
