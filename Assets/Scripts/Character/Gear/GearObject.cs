using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Gear", menuName = "Gears/GearObject")]
public class GearObject : ScriptableObject
{
    [SerializeField] private ClassType classType;
    [SerializeField] private GearType gearType;
    public CharacterAbility attackAbility;

    [SerializeField] private string itemName;
    [SerializeField] private Image itemIcon;
    [SerializeField] private int pAtk;
    [SerializeField] private int mAtk;
    [SerializeField] private float atkSpeed;
    [SerializeField] private float critRate;
    [SerializeField] private float critDmg;
    [SerializeField] private int vitality;
    [SerializeField] private int wisdom;
    [SerializeField] private int strength;
    [SerializeField] private int intelligence;
    [SerializeField] private int dexterity;

    public enum ClassType
    {
        Any,
        Warrior,
        Mage,
        Ranger
    }

    public enum GearType
    {
        Weapon,
        Helmet,
        Armor
    }


    public void Attack(int attackID, Transform playerPos, Transform mousePos, string fromEntity, int damage)
    {
        attackAbility.Use(attackID, playerPos, fromEntity, damage, null);
    }

    public string GetClass()
    {
        return classType.ToString();
    }

    public string GetGearType()
    {
        return gearType.ToString();
    }

    public void AddStats()
    {
        GameManager.singleton.playerStats.pAtk.AddBonusValue(pAtk);
        GameManager.singleton.playerStats.mAtk.AddBonusValue(mAtk);
        //GameManager.singleton.playerStats.atkSpeed.AddBonusValue(atkSpeed);
        //GameManager.singleton.playerStats.critRate.AddBonusValue(critRate);
        //GameManager.singleton.playerStats.critDmg.AddBonusValue(critDmg);
        GameManager.singleton.playerStats.vitality.AddBonusValue(vitality);
        GameManager.singleton.playerStats.wisdom.AddBonusValue(wisdom);
        GameManager.singleton.playerStats.strength.AddBonusValue(strength);
        GameManager.singleton.playerStats.dexterity.AddBonusValue(dexterity);
        GameManager.singleton.playerStats.intelligence.AddBonusValue(intelligence);

        GameManager.singleton.playerStats.UpdateStatTexts();
    }

    public void RemoveStats()
    {
        GameManager.singleton.playerStats.pAtk.AddBonusValue(-pAtk);
        GameManager.singleton.playerStats.mAtk.AddBonusValue(-mAtk);
        //GameManager.singleton.playerStats.atkSpeed.AddBonusValue(-atkSpeed);
        //GameManager.singleton.playerStats.critRate.AddBonusValue(-critRate);
        //GameManager.singleton.playerStats.critDmg.AddBonusValue(-critDmg);
        GameManager.singleton.playerStats.vitality.AddBonusValue(-vitality);
        GameManager.singleton.playerStats.wisdom.AddBonusValue(-wisdom);
        GameManager.singleton.playerStats.strength.AddBonusValue(-strength);
        GameManager.singleton.playerStats.dexterity.AddBonusValue(-dexterity);
        GameManager.singleton.playerStats.intelligence.AddBonusValue(-intelligence);

        GameManager.singleton.playerStats.UpdateStatTexts();
    }
}
