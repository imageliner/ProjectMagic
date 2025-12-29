using System.Collections.Generic;
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
    [SerializeField] private int comboCount = 1;
    [SerializeField] private int pAtk;
    [SerializeField] private int mAtk;
    [SerializeField] private int pDef;
    [SerializeField] private int mDef;
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

    public string GetStats()
    {
        List<string> stats = new List<string>();

        //if (pAtk > 0) stats.Add("pAtk " + pAtk);
        //if (mAtk > 0) stats.Add("mAtk " + mAtk);
        if (vitality > 0) stats.Add("Vitality " + vitality);
        if (wisdom > 0) stats.Add("Wisdom " + wisdom);
        if (strength > 0) stats.Add("Strength " + strength);
        if (intelligence > 0) stats.Add("Intelligence " + intelligence);
        if (dexterity > 0) stats.Add("Dexterity " + dexterity);
        if (atkSpeed > 0) stats.Add("AtkSpeed " + atkSpeed);
        if (critRate > 0) stats.Add("CritRate " + critRate);
        if (critDmg > 0) stats.Add("CritDmg " + critDmg);

        return string.Join(" ", stats) + " ";
    }

    public int GetDamage()
    {
        if (classType == ClassType.Mage)
            return mAtk;
        else
            return pAtk;
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
        GameManager.singleton.playerStats.pDef.AddBonusValue(pDef);
        GameManager.singleton.playerStats.mDef.AddBonusValue(mDef);
        //GameManager.singleton.playerStats.atkSpeed.AddBonusValue(atkSpeed);
        //GameManager.singleton.playerStats.critRate.AddBonusValue(critRate);
        //GameManager.singleton.playerStats.critDmg.AddBonusValue(critDmg);
        GameManager.singleton.playerStats.vitality.AddBonusValue(vitality);
        GameManager.singleton.playerStats.wisdom.AddBonusValue(wisdom);
        GameManager.singleton.playerStats.strength.AddBonusValue(strength);
        GameManager.singleton.playerStats.dexterity.AddBonusValue(dexterity);
        GameManager.singleton.playerStats.intelligence.AddBonusValue(intelligence);

        GameManager.singleton.player.SetComboLimit(Mathf.Clamp(comboCount, 1, 100));

        GameManager.singleton.playerStats.StatsUpdated?.Invoke();
    }

    public void RemoveStats()
    {
        GameManager.singleton.playerStats.pAtk.AddBonusValue(-pAtk);
        GameManager.singleton.playerStats.mAtk.AddBonusValue(-mAtk);
        GameManager.singleton.playerStats.pDef.AddBonusValue(-pDef);
        GameManager.singleton.playerStats.mDef.AddBonusValue(-mDef);
        //GameManager.singleton.playerStats.atkSpeed.AddBonusValue(-atkSpeed);
        //GameManager.singleton.playerStats.critRate.AddBonusValue(-critRate);
        //GameManager.singleton.playerStats.critDmg.AddBonusValue(-critDmg);
        GameManager.singleton.playerStats.vitality.AddBonusValue(-vitality);
        GameManager.singleton.playerStats.wisdom.AddBonusValue(-wisdom);
        GameManager.singleton.playerStats.strength.AddBonusValue(-strength);
        GameManager.singleton.playerStats.dexterity.AddBonusValue(-dexterity);
        GameManager.singleton.playerStats.intelligence.AddBonusValue(-intelligence);

        GameManager.singleton.player.SetComboLimit(1);

        GameManager.singleton.playerStats.StatsUpdated?.Invoke();
    }
}
