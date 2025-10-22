using UnityEngine;

[CreateAssetMenu(fileName = "BaseStatData", menuName = "Scriptable Objects/BaseStatData")]
public class BaseStatData : ScriptableObject
{
    [Header("Main Stats")]
    public int healthBase = 100;
    public int staminaBase = 50;
    public int manaBase = 50;
    public int pDefenceBase = 1;
    public int mDefenceBase = 1;
    public int pAttackBase = 1;
    public int mAttackBase = 1;

    [Header("Defining Stats")]
    public int strengthBase = 1;
    public int dexterityBase = 1;
    public int intelligenceBase = 1;
    public int luckBase = 1;

    //[Header("Extra Stats")]




}
