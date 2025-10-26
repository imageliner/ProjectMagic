using UnityEngine;

public class CharacterStats_Old : MonoBehaviour
{
    [Header("Base Stats")]
    public int baseHealth;
    public int baseStamina;
    public int baseMana;
    public int basePhysAtk;
    public int basePhysDef;
    public int baseMagAtk;
    public int baseMagDef;

    public Stat constitution;
    public Stat endurance;
    public Stat wisdom;
    public Stat strength;
    public Stat dexterity;
    public Stat intelligence;

    [Header("Current Stats")]
    public int maxHealth;
    public int maxStamina;
    public int maxMana;
    public int totalConstitution;
    public int totalEndurance;
    public int totalWisdom;
    public int totalStrength;
    public int totalDexterity;
    public int totalIntelligence;
    public int physAttack;
    public int physDefence;
    public int magAttack;
    public int magDefence;

    public int currentHealth;
    public int currentStamina;
    public int currentMana;

    [Header("Level Stats")]
    public int level;
    public float experience;



    private void Awake()
    {
        
    }

    private void Start()
    {
        InitializStat();
    }

    public void InitializStat()
    {
        maxHealth = baseHealth;
        currentHealth = maxHealth;
        maxStamina = baseStamina;
        currentStamina = maxStamina;
        maxMana = baseMana;
        currentMana = maxMana;

        SetCurrentStatValues();
    }

    public void SetCurrentStatValues()
    {
        HealthCalculation();
        StamCalculation();
        ManaCalculation();
        PhysAtkCalculation();
        PhysDefenceCalculation();
        MagAtkCalculation();
        MagDefenceCalculation();


        totalConstitution = constitution.GetSumValue();
        totalEndurance = endurance.GetSumValue();
        totalWisdom = wisdom.GetSumValue();
        totalStrength = strength.GetSumValue();
        totalDexterity = dexterity.GetSumValue();
        totalIntelligence = intelligence.GetSumValue();
    }

    public void SubtractStamina(int value)
    {
        currentStamina -= value;
    }

    public void TakeDamageCalculation(int dmgAmount)
    {
        currentHealth -= dmgAmount;
    }

    public void TakeHealCalculation(int healAmount)
    {
        if (currentHealth <= maxHealth)
        {
            currentHealth += healAmount;
        }
        if (healAmount + currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void HealthCalculation()
    {
        float bonusHealth =  ( (baseHealth * totalConstitution) / 1.5f);
        maxHealth = Mathf.CeilToInt(baseHealth + bonusHealth);
    }
    private void StamCalculation()
    {
        float bonusStam = ((baseStamina * totalEndurance) / 1.5f);
        maxStamina = Mathf.CeilToInt(baseStamina + bonusStam);
    }

    private void ManaCalculation()
    {
        float bonusMana = ((baseMana * totalWisdom) / 1.5f);
        maxMana = Mathf.CeilToInt(baseMana + bonusMana);
    }
    private void PhysAtkCalculation()
    {
        float bonusPhysAtk = ((basePhysAtk * totalStrength) / 1.5f);
        physAttack = Mathf.CeilToInt(basePhysAtk + bonusPhysAtk);
    }
    private void PhysDefenceCalculation()
    {
        float bonusPhysDef = ((basePhysDef * (totalStrength * totalConstitution) / 2f) / 1.5f);
        physDefence = Mathf.CeilToInt(basePhysDef + bonusPhysDef);
    }
    private void MagAtkCalculation()
    {
        float bonusMagAtk = ((baseMagAtk * totalIntelligence) / 1.5f);
        magAttack = Mathf.CeilToInt(baseMagAtk + bonusMagAtk);
    }
    private void MagDefenceCalculation()
    {
        float bonusMagDef = ((baseMagDef * (totalIntelligence * totalWisdom) / 2f) / 1.5f);
        magDefence = Mathf.CeilToInt(baseMagDef + bonusMagDef);
    }
}
