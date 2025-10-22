using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int baseValue;
    //[SerializeField] private int newValue;

    public int GetValue()
    {
        return baseValue;
    }
}
