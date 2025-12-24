using UnityEngine;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    public StatCalculations statCalcs;
    [SerializeField] int vitalityToHealthConversion = 5;
    [SerializeField] int dexToStamConversion = 1;
    [SerializeField] int wisdomToManaConversion = 2;
    [SerializeField] int strengthToPhysAtk = 1;
    [SerializeField] float strengthToPhysDef = 1.2f;
    [SerializeField] int intelligenceToMAtk = 1;
    [SerializeField] float intelligenceToMDef = 1.2f;
    [SerializeField] float dexToAtkSpeed = 1.2f;

    [SerializeField] protected int initialHealth;
    [SerializeField] protected int initialStamina;
    [SerializeField] protected int initialMana;

    public Resource health { get; protected set; }
    public Resource stamina { get; protected set; }
    public Resource mana { get; protected set; }
    public Stat vitality { get; protected set; }
    public Stat wisdom { get; protected set; }
    public Stat strength { get; protected set; }
    public Stat dexterity { get; protected set; }
    public Stat intelligence { get; protected set; }

    public Stat pAtk { get; protected set; }
    public Stat pDef { get; protected set; }
    public Stat mAtk { get; protected set; }
    public Stat mDef { get; protected set; }
    public Stat atkSpeed { get; protected set; }
    public Stat critRate { get; protected set; }
    public Stat critDmg { get; protected set; }

    public int maxHealth
    {
        get
        {
            return health.GetTotalValue(vitality, vitalityToHealthConversion);
        }
    }

    public int maxStamina
    {
        get
        {
            return stamina.GetTotalValue(dexterity, dexToStamConversion);
        }
    }

    public int maxMana
    {
        get
        {
            return mana.GetTotalValue(wisdom, wisdomToManaConversion);
        }
    }

    public int finalPhysAtk
    {
        get
        {
            return pAtk.GetTotalValue() + strength.GetTotalValueTest(strength, strengthToPhysAtk);
        }
    }

    public int finalPhysDef
    {
        get
        {
            return pDef.GetTotalValue() + Mathf.CeilToInt(strength.GetTotalValueTest(strength, 2));
        }
    }

    public int finalMAtk
    {
        get
        {
            return mAtk.GetTotalValue() + intelligence.GetTotalValueTest(intelligence, intelligenceToMAtk);
        }
    }

    public int finalMDef
    {
        get
        {
            return mDef.GetTotalValue() +  Mathf.CeilToInt(intelligence.GetTotalValueTest(intelligence, 2));
        }
    }

    public float finalAtkSpeed
    {
        get
        {
            return 1 + (dexterity.GetTotalValue() * 0.05f);
        }
    }

    protected virtual void Awake()
    {
        InitializeStats();

    }

    protected virtual void InitializeStats()
    {
        health = new Resource();
        stamina = new Resource();
        mana = new Resource();
        health.SetBaseValue(initialHealth);
        health.SetCurrentValue(initialHealth);
        mana.SetBaseValue(initialMana);
        mana.SetCurrentValue(initialMana);
        vitality = new Stat();
        wisdom = new Stat();
        strength = new Stat();
        dexterity = new Stat();
        intelligence = new Stat();
        pAtk = new Stat();
        pDef = new Stat();
        mAtk = new Stat();
        mDef = new Stat();
        atkSpeed = new Stat();
        critRate = new Stat();
        critDmg = new Stat();
    }

}
