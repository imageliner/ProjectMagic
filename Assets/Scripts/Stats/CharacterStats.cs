using UnityEngine;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] int baseVitality_perLevel = 1;
    [SerializeField] int baseVitality_Offset = 0;
    [SerializeField] int vitalityToHealthConversion = 10;

    [SerializeField] protected int initialHealth;

    public Resource health { get; protected set; }
    public Stat vitality { get; protected set; }
    public Stat wisdom { get; protected set; }
    public Stat strength { get; protected set; }
    public Stat dexterity { get; protected set; }
    public Stat intelligence { get; protected set; }

    public int maxHealth
    {
        get
        {
            return health.GetTotalValue(vitality, vitalityToHealthConversion);
        }
    }

    protected virtual void InitializeStats()
    {
        health = new Resource();
        health.SetBaseValue(initialHealth);
        health.SetCurrentValue(initialHealth);
        vitality = new Stat();
        wisdom = new Stat();
        strength = new Stat();
        dexterity = new Stat();
        intelligence = new Stat();
    }

}
