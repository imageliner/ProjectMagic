using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private BaseStatData baseStatData;
    [Header("Main Stats")]
    public int health;

    [Header("Defining Stats")]
    public int constitution;
    public int endurance;
    public int wisdom;
    public int strength;
    public int dexterity;
    public int intelligence;

    private void Awake()
    {
        GetStatValues();
    }

    private void GetStatValues()
    {
        constitution = baseStatData.constitution.GetValue();
        endurance = baseStatData.endurance.GetValue();
        wisdom = baseStatData.wisdom.GetValue();
        strength = baseStatData.strength.GetValue();
        dexterity = baseStatData.dexterity.GetValue();
        intelligence = baseStatData.intelligence.GetValue();
    }
}
